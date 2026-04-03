using FoodServiceOccupancyForecast.Core.Enums;

namespace FoodServiceOccupancyForecast.Core.Entities;

public class BookingResult
{
    public bool Success { get; set; }
    public int? BookingId { get; set; }
    public string? ErrorMessage { get; set; }
    public BookingStatus Status { get; set; }
}