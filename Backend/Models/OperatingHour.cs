using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class OperatingHour
{
    public int OperatingHourId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public int DayOfWeek { get; set; }
    
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
}