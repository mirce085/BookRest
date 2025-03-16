using BookRest.Dtos.Reservation;
using FluentValidation;

namespace BookRest.Validators.Reservation;

public class ReservationCreateValidator : AbstractValidator<ReservationCreateDto>
{
    public ReservationCreateValidator()
    {
        RuleFor(r => r.UserId).GreaterThan(0).WithMessage("UserId is required.");
        RuleFor(r => r.RestaurantId).GreaterThan(0).WithMessage("RestaurantId is required.");
        RuleFor(r => r.TableId).GreaterThan(0).WithMessage("TableId is required.");

        RuleFor(r => r.ReservationDateTime)
            .GreaterThan(DateTime.Now).WithMessage("Reservation time must be in the future.");

        RuleFor(r => r.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(r => r.NumberOfGuests)
            .GreaterThan(0).WithMessage("Number of guests must be at least 1.");

        RuleFor(r => r.SpecialRequests)
            .MaximumLength(1000).When(r => !string.IsNullOrEmpty(r.SpecialRequests));
    }
}