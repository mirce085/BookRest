using BookRest.Dtos.Notification;
using FluentValidation;

namespace BookRest.Validators.Notification;

public class NotificationCreateValidator : AbstractValidator<NotificationCreateDto>
{
    public NotificationCreateValidator()
    {
        RuleFor(x => x.NotificationType)
            .IsInEnum()
            .WithMessage("Invalid Notification Type");

        RuleFor(x => x.ScheduledTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("ScheduledTime must be in the future.");
    }
}