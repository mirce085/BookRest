using BookRest.Dtos.OperatingHour;
using FluentValidation;

namespace BookRest.Validators.OperatingHour;

public class OperatingHourCreateValidator : AbstractValidator<OperatingHourCreateDto>
{
    public OperatingHourCreateValidator()
    {
        RuleFor(o => o.RestaurantId)
            .GreaterThan(0).WithMessage("RestaurantId is required.");
        
        RuleFor(o => o.DayOfWeek)
            .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 (Sunday) and 6 (Saturday).");

        RuleFor(o => o.OpenTime)
            .LessThan(o => o.CloseTime).WithMessage("OpenTime must be earlier than CloseTime.");
    }
}