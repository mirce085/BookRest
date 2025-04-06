using AutoMapper;
using BookRest.Data;
using BookRest.Dtos.MenuItem;
using BookRest.Dtos.Notification;
using BookRest.Models;
using BookRest.Other;
using BookRest.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class NotificationService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IMapper mapper,
    IValidator<NotificationCreateDto> createValidator,
    IValidator<NotificationUpdateDto> updateValidator) : INotificationService
{
    public async Task<OperationResult<NotificationDisplayDto>> ScheduleNotificationAsync(NotificationCreateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<NotificationDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        bool reservationExists = await dbContext.Reservations.AnyAsync(r => r.ReservationId == dto.ReservationId);
        if (!reservationExists)
            return OperationResult<NotificationDisplayDto>.Fail("Reservation does not exist.");

        var notification = mapper.Map<Notification>(dto);

        dbContext.Notifications.Add(notification);
        await dbContext.SaveChangesAsync();

        var notificationDto = mapper.Map<NotificationDisplayDto>(notification);

        return OperationResult<NotificationDisplayDto>.Ok(notificationDto);
    }

    public async Task<OperationResult<NotificationDisplayDto>> GetNotificationAsync(int notificationId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var notification = await dbContext.Notifications.FindAsync(notificationId);
        if (notification == null)
            return OperationResult<NotificationDisplayDto>.Fail("Notification not found.");

        var dto = mapper.Map<NotificationDisplayDto>(notification);

        return OperationResult<NotificationDisplayDto>.Ok(dto);
    }

    public async Task<OperationResult<NotificationDisplayDto>> UpdateNotificationAsync(int notificationId,
        NotificationUpdateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<NotificationDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var notification = await dbContext.Notifications.FindAsync(notificationId);
        if (notification == null)
            return OperationResult<NotificationDisplayDto>.Fail("Notification not found.");

        if (dto.NotificationType.HasValue)
            notification.NotificationType = dto.NotificationType.Value;
        if (dto.ScheduledTime.HasValue)
            notification.ScheduledTime = dto.ScheduledTime.Value;
        if (dto.SentStatus.HasValue)
            notification.SentStatus = dto.SentStatus.Value;
        if (dto.SentTime.HasValue)
            notification.SentTime = dto.SentTime.Value;

        await dbContext.SaveChangesAsync();

        var updatedDto = mapper.Map<NotificationDisplayDto>(notification);
        
        return OperationResult<NotificationDisplayDto>.Ok(updatedDto);
    }

    public async Task<OperationResult<bool>> DeleteNotificationAsync(int notificationId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var notification = await dbContext.Notifications.FindAsync(notificationId);
        if (notification == null)
            return OperationResult<bool>.Fail("Notification not found.");

        dbContext.Notifications.Remove(notification);
        await dbContext.SaveChangesAsync();

        return OperationResult<bool>.Ok(true);
    }
}