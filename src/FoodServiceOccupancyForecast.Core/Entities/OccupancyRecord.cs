namespace FoodServiceOccupancyForecast.Core.Entities;

public class OccupancyRecord
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int TotalGuests { get; set; }           // Всего гостей в зале
    public int OccupiedTables { get; set; }        // Занятых столов
    public int FreeTables { get; set; }            // Свободных столов
    public double OccupancyPercent { get; set; }   // Процент загрузки (0-100)
}