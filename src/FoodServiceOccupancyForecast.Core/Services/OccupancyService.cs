using FoodServiceOccupancyForecast.Core.Entities;
using FoodServiceOccupancyForecast.Core.Enums;
using FoodServiceOccupancyForecast.Core.Interfaces;

namespace FoodServiceOccupancyForecast.Core.Services;

public class OccupancyService : IOccupancyService
{
    private readonly ITableRepository _tableRepository;
    private readonly IOccupancyRepository _occupancyRepository;

    public OccupancyService(ITableRepository tableRepository, IOccupancyRepository occupancyRepository)
    {
        _tableRepository = tableRepository;
        _occupancyRepository = occupancyRepository;
    }

    public async Task<OccupancyStats> GetCurrentOccupancyAsync()
    {
        var tables = await _tableRepository.GetAllAsync();
        var activeTables = tables.Where(t => t.IsActive).ToList();

        var occupiedTables = activeTables.Count(t => t.Status == TableStatus.Occupied);
        var freeTables = activeTables.Count(t => t.Status == TableStatus.Free);
        var totalTables = activeTables.Count;

        var occupancyPercent = totalTables > 0 ? (double)occupiedTables / totalTables * 100 : 0;

        return new OccupancyStats
        {
            TotalGuests = 0, // TODO: будет из видеоаналитики
            FreeTables = freeTables,
            OccupiedTables = occupiedTables,
            TotalTables = totalTables,
            OccupancyPercent = occupancyPercent,
            Timestamp = DateTime.UtcNow
        };
    }

    public async Task<PeakHoursResult> GetPeakHoursAsync(DateTime date)
    {
        // TODO: анализ исторических данных из базы
        // Пока возвращаем примерные данные
        return new PeakHoursResult
        {
            Date = date,
            PeakHours = new List<PeakHour>
            {
                new() { Hour = 12, OccupancyPercent = 70, Recommendation = "Нужен 1 дополнительный официант" },
                new() { Hour = 13, OccupancyPercent = 85, Recommendation = "Нужны 2 дополнительных официанта" },
                new() { Hour = 18, OccupancyPercent = 90, Recommendation = "Нужны 2 дополнительных официанта" },
                new() { Hour = 19, OccupancyPercent = 95, Recommendation = "Нужны 3 дополнительных официанта" }
            }
        };
    }

    public async Task RecordOccupancyAsync(int totalGuests, int occupiedTables, int freeTables)
    {
        var record = new OccupancyRecord
        {
            Timestamp = DateTime.UtcNow,
            TotalGuests = totalGuests,
            OccupiedTables = occupiedTables,
            FreeTables = freeTables,
            OccupancyPercent = freeTables + occupiedTables > 0
                ? (double)occupiedTables / (freeTables + occupiedTables) * 100
                : 0
        };

        await _occupancyRepository.AddAsync(record);
    }
}