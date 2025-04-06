using BookRest.Dtos.Notification;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface INotificationService
{
    Task<OperationResult<NotificationDisplayDto>> ScheduleNotificationAsync(NotificationCreateDto dto);
    Task<OperationResult<NotificationDisplayDto>> GetNotificationAsync(int notificationId);
    Task<OperationResult<NotificationDisplayDto>> UpdateNotificationAsync(int notificationId, NotificationUpdateDto dto);
    Task<OperationResult<bool>> DeleteNotificationAsync(int notificationId);
}