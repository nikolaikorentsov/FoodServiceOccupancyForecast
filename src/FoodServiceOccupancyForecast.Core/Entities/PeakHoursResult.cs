namespace FoodServiceOccupancyForecast.Core.Entities;

public class PeakHoursResult
{
    public DateTime Date { get; set; }
    public List<PeakHour> PeakHours { get; set; } = new();
}

public class PeakHour
{
    public int Hour { get; set; }           // Час (0-23)
    public int OccupancyPercent { get; set; } // Загрузка в этот час
    public string? Recommendation { get; set; } // Рекомендация по персоналу
}