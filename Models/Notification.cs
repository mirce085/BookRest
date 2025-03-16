using System.ComponentModel.DataAnnotations;
using BookRest.Models.Enums;

namespace BookRest.Models;

public class Notification
{
    public int NotificationId { get; set; }
    
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    
    public NotificationType NotificationType { get; set; }  // e.g. 'Email', 'SMS'

    public DateTime ScheduledTime { get; set; }
    
    public NotificationStatus SentStatus { get; set; }

    public DateTime? SentTime { get; set; }
}