namespace FoodServiceOccupancyForecast.Core.Enums;

public enum BookingStatus
{
    Pending = 0,     // Ожидает подтверждения
    Confirmed = 1,   // Подтвержден
    Cancelled = 2,   // Отменен
    Completed = 3    // Выполнен (гости пришли)
}