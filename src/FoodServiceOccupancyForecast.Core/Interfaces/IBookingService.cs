using FoodServiceOccupancyForecast.Core.Entities;

namespace FoodServiceOccupancyForecast.Core.Interfaces;

public interface IBookingService
{
    Task<BookingResult> CreateBookingAsync(BookingRequest request);
    Task<bool> IsTableAvailableAsync(int tableId, DateTime dateTime, int personsCount);
    Task<IEnumerable<Booking>> GetBookingsForDateAsync(DateTime date);
    Task<BookingResult> ConfirmBookingAsync(int bookingId);
    Task<BookingResult> CancelBookingAsync(int bookingId);
}