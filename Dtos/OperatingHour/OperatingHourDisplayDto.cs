namespace BookRest.Dtos.OperatingHour;

public class OperatingHourDisplayDto
{
    public int OperatingHourId { get; set; }
    public int RestaurantId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
}