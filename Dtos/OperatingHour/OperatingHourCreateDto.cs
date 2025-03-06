namespace BookRest.Dtos.OperatingHour;

public class OperatingHourCreateDto
{
    public int RestaurantId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
}