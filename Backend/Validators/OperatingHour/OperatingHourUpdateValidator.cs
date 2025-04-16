using BookRest.Dtos.OperatingHour;
using FluentValidation;

namespace BookRest.Validators.OperatingHour;

public class OperatingHourUpdateValidator : AbstractValidator<OperatingHourUpdateDto>
{
    public OperatingHourUpdateValidator()
    {
        RuleFor(o => o.DayOfWeek)
            .InclusiveBetween(0, 6).When(o => o.DayOfWeek.HasValue);

        RuleFor(o => o.OpenTime)
            .LessThan(o => o.CloseTime)
            .When(o => o.OpenTime.HasValue && o.CloseTime.HasValue)
            .WithMessage("OpenTime must be earlier than CloseTime.");
        
        RuleFor(dto => dto)
            .Must(dto => dto.DayOfWeek != null || dto.OpenTime != null).WithMessage("You must change at least one field.");
    }
}