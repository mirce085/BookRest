using BookRest.Dtos.Reservation;
using FluentValidation;

namespace BookRest.Validators.Reservation;

public class ReservationUpdateValidator : AbstractValidator<ReservationUpdateDto>
{
    public ReservationUpdateValidator()
    {
        RuleFor(r => r.ReservationDateTime)
            .GreaterThan(DateTime.Now)
            .When(r => r.ReservationDateTime.HasValue);

        RuleFor(r => r.Duration)
            .GreaterThan(0)
            .When(r => r.Duration.HasValue);

        RuleFor(r => r.NumberOfGuests)
            .GreaterThan(0).When(r => r.NumberOfGuests.HasValue);

        RuleFor(r => r.SpecialRequests)
            .MaximumLength(1000)
            .When(r => !string.IsNullOrEmpty(r.SpecialRequests));

        RuleFor(r => r.Status)
            .Must(status => 
                status == "Pending" || 
                status == "Confirmed" || 
                status == "Cancelled")
            .WithMessage("Status must be one of 'Pending', 'Confirmed', 'Cancelled'.")
            .When(r => !string.IsNullOrEmpty(r.Status));
        
        RuleFor(dto => dto)
            .Must(dto => dto.ReservationDateTime != null || dto.Duration != null || dto.NumberOfGuests != null || dto.SpecialRequests != null || dto.Status != null).WithMessage("You must change at least one field.");
    }
}