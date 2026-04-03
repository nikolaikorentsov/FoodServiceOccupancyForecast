using FoodServiceOccupancyForecast.Core.Entities;
using FoodServiceOccupancyForecast.Core.Enums;

namespace FoodServiceOccupancyForecast.Core.Interfaces;

public interface ITableRepository
{
    Task<Table?> GetByIdAsync(int id);
    Task<IEnumerable<Table>> GetAllAsync();
    Task<IEnumerable<Table>> GetActiveTablesAsync();
    Task<IEnumerable<Table>> GetFreeTablesAsync(DateTime dateTime);
    Task<int> GetTotalTablesCountAsync();
    Task<int> GetOccupiedTablesCountAsync();
    Task UpdateStatusAsync(int tableId, TableStatus status);
    Task AddAsync(Table table);
    Task UpdateAsync(Table table);
}