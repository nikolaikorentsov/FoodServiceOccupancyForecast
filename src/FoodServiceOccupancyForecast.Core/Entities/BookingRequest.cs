namespace FoodServiceOccupancyForecast.Core.Entities;

public class BookingRequest
{
    public int TableId { get; set; }
    public DateTime DateTime { get; set; }
    public int PersonsCount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }
}