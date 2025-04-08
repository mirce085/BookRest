using BookRest.Dtos.Notification;
using BookRest.Models.Enums;
using FluentValidation;

namespace BookRest.Validators.Notification;

public class NotificationUpdateValidator : AbstractValidator<NotificationUpdateDto>
{
    public NotificationUpdateValidator()
    {
        RuleFor(x => x)
            .Must(dto => dto.NotificationType.HasValue || dto.ScheduledTime.HasValue || dto.SentStatus != null ||
                         dto.SentTime.HasValue)
            .WithMessage("At least one field must be provided for update.");


        RuleFor(x => x.NotificationType!.Value)
            .IsInEnum()
            .When(x => x.NotificationType.HasValue);


        RuleFor(x => x.ScheduledTime!.Value)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("ScheduledTime must be in the future.")
            .When(x => x.ScheduledTime.HasValue);


        RuleFor(x => x.SentStatus)
            .IsInEnum().WithMessage("Invalid Sent Status")
            .When(x => x.SentStatus != null);


        RuleFor(x => x.SentTime)
            .NotNull().WithMessage("SentTime must be provided when SentStatus is 'Sent'.")
            .When(x => x.SentStatus == NotificationStatus.Sent);


        RuleFor(x => x.SentTime!.Value)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("SentTime cannot be in the future.")
            .When(x => x.SentStatus.HasValue);
    }
};