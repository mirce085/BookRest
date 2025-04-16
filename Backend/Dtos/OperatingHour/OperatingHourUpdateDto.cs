namespace BookRest.Dtos.OperatingHour;

public class OperatingHourUpdateDto
{
    public int? DayOfWeek { get; set; }
    public TimeSpan? OpenTime { get; set; }
    public TimeSpan? CloseTime { get; set; }
}