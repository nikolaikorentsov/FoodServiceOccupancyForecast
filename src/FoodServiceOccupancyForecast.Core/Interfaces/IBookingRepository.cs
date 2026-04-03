using FoodServiceOccupancyForecast.Core.Entities;
using FoodServiceOccupancyForecast.Core.Enums;

namespace FoodServiceOccupancyForecast.Core.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(int id);
    Task<IEnumerable<Booking>> GetByTableAndTimeAsync(int tableId, DateTime dateTime);
    Task<IEnumerable<Booking>> GetBookingsForDateAsync(DateTime date);
    Task<IEnumerable<Booking>> GetPendingBookingsAsync();
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task UpdateStatusAsync(int bookingId, BookingStatus status);
}