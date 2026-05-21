using FoodServiceOccupancyForecast.Core.Entities;
using FoodServiceOccupancyForecast.Core.Enums;
using FoodServiceOccupancyForecast.Core.Interfaces;

namespace FoodServiceOccupancyForecast.Core.Services;

public class BookingService : IBookingService
{
    private readonly ITableRepository _tableRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(ITableRepository tableRepository, IBookingRepository bookingRepository)
    {
        _tableRepository = tableRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<bool> IsTableAvailableAsync(int tableId, DateTime dateTime, int personsCount)
    {
        // 1. Проверяем, существует ли стол
        var table = await _tableRepository.GetByIdAsync(tableId);
        if (table == null || !table.IsActive)
            return false;

        // 2. Проверяем вместимость
        if (table.Capacity < personsCount)
            return false;

        // 3. Проверяем брони на это время (плюс-минус 1 час)
        var startTime = dateTime.AddHours(-1);
        var endTime = dateTime.AddHours(1);

        var bookings = await _bookingRepository.GetByTableAndTimeAsync(tableId, dateTime);

        // Проверяем, есть ли подтвержденные брони в этот период
        var hasConflict = bookings.Any(b =>
            b.Status == BookingStatus.Confirmed &&
            b.BookingTime >= startTime &&
            b.BookingTime <= endTime);

        return !hasConflict;
    }

    public async Task<BookingResult> CreateBookingAsync(BookingRequest request)
    {
        // 1. Проверяем доступность стола
        var isAvailable = await IsTableAvailableAsync(request.TableId, request.DateTime, request.PersonsCount);

        if (!isAvailable)
        {
            return new BookingResult
            {
                Success = false,
                ErrorMessage = "Стол недоступен на выбранное время. Пожалуйста, выберите другое время или столик."
            };
        }

        // 2. Создаем бронь
        var booking = new Booking
        {
            TableId = request.TableId,
            CustomerName = request.CustomerName,
            Phone = request.Phone,
            BookingTime = request.DateTime,
            PersonsCount = request.PersonsCount,
            Status = BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            Notes = request.Notes
        };

        await _bookingRepository.AddAsync(booking);

        // 3. Резервируем стол (меняем статус на Reserved)
        await _tableRepository.UpdateStatusAsync(request.TableId, TableStatus.Reserved);

        return new BookingResult
        {
            Success = true,
            BookingId = booking.Id,
            Status = booking.Status
        };
    }

    public async Task<IEnumerable<Booking>> GetBookingsForDateAsync(DateTime date)
    {
        return await _bookingRepository.GetBookingsForDateAsync(date);
    }

    public async Task<BookingResult> ConfirmBookingAsync(int bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
        {
            return new BookingResult
            {
                Success = false,
                ErrorMessage = "Бронь не найдена"
            };
        }

        if (booking.Status != BookingStatus.Pending)
        {
            return new BookingResult
            {
                Success = false,
                ErrorMessage = $"Невозможно подтвердить бронь со статусом {booking.Status}"
            };
        }

        booking.Status = BookingStatus.Confirmed;
        await _bookingRepository.UpdateAsync(booking);

        return new BookingResult
        {
            Success = true,
            BookingId = booking.Id,
            Status = booking.Status
        };
    }

    public async Task<BookingResult> CancelBookingAsync(int bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
        {
            return new BookingResult
            {
                Success = false,
                ErrorMessage = "Бронь не найдена"
            };
        }

        if (booking.Status == BookingStatus.Completed)
        {
            return new BookingResult
            {
                Success = false,
                ErrorMessage = "Нельзя отменить уже выполненную бронь"
            };
        }

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.UpdateAsync(booking);

        // Освобождаем стол
        await _tableRepository.UpdateStatusAsync(booking.TableId, TableStatus.Free);

        return new BookingResult
        {
            Success = true,
            BookingId = booking.Id,
            Status = booking.Status
        };
    }
}