using FoodServiceOccupancyForecast.Core.Entities;

namespace FoodServiceOccupancyForecast.Core.Interfaces;

public interface IOccupancyRepository
{
    Task AddAsync(OccupancyRecord record);
    Task<IEnumerable<OccupancyRecord>> GetRecordsForDateAsync(DateTime date);
    Task<IEnumerable<OccupancyRecord>> GetRecordsForPeriodAsync(DateTime start, DateTime end);
}