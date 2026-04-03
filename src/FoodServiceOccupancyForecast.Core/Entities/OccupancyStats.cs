namespace FoodServiceOccupancyForecast.Core.Entities;

public class OccupancyStats
{
    public int TotalGuests { get; set; }
    public int FreeTables { get; set; }
    public int OccupiedTables { get; set; }
    public int TotalTables { get; set; }
    public double OccupancyPercent { get; set; }
    public DateTime Timestamp { get; set; }
}