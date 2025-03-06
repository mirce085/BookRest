using BookRest.Models.Enums;

namespace BookRest.Dtos.Notification;

public class NotificationCreateDto
{
    public int ReservationId { get; set; }
    public NotificationType NotificationType { get; set; }
    public DateTime ScheduledTime { get; set; }
}