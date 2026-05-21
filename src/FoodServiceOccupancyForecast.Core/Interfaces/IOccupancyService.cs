using FoodServiceOccupancyForecast.Core.Entities;

namespace FoodServiceOccupancyForecast.Core.Interfaces;

public interface IOccupancyService
{
    Task<OccupancyStats> GetCurrentOccupancyAsync();
    Task<PeakHoursResult> GetPeakHoursAsync(DateTime date);
    Task RecordOccupancyAsync(int totalGuests, int occupiedTables, int freeTables);
}