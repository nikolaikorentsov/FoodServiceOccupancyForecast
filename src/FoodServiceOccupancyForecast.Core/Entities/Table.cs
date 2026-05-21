using FoodServiceOccupancyForecast.Core.Enums;

namespace FoodServiceOccupancyForecast.Core.Entities;

public class Table
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;      // "Столик у окна"
    public int Capacity { get; set; }                      // Количество мест
    public int X { get; set; }                             // Координата X на карте
    public int Y { get; set; }                             // Координата Y на карте
    public TableStatus Status { get; set; } = TableStatus.Free;
    public bool IsActive { get; set; } = true;

    // Навигационное свойство (связь с бронированиями)
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}