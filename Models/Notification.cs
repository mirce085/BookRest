using System.ComponentModel.DataAnnotations;
using BookRest.Models.Enums;

namespace BookRest.Models;

public class Notification
{
    [Key]
    public int NotificationId { get; set; }
    
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }

    [Required]
    [MaxLength(50)]
    public NotificationType NotificationType { get; set; }  // e.g. 'Email', 'SMS'

    public DateTime ScheduledTime { get; set; }

    [Required]
    [MaxLength(50)]
    public NotificationStatus SentStatus { get; set; }

    public DateTime? SentTime { get; set; }
}