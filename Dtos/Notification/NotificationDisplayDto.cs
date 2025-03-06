using BookRest.Models.Enums;

namespace BookRest.Dtos.Notification;

public class NotificationDisplayDto
{
    public int NotificationId { get; set; }
    public int ReservationId { get; set; }
    public NotificationType NotificationType { get; set; }
    public DateTime ScheduledTime { get; set; }
    public NotificationStatus SentStatus { get; set; }
    public DateTime? SentTime { get; set; }
}