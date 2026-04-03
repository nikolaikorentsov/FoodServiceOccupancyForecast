using FoodServiceOccupancyForecast.Core.Enums;

namespace FoodServiceOccupancyForecast.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public int PersonsCount { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }

    // Навигационное свойство
    public Table? Table { get; set; }
}