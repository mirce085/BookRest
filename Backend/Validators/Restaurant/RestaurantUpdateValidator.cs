using BookRest.Dtos.Restaurant;
using FluentValidation;

namespace BookRest.Validators.Restaurant;

public class RestaurantUpdateValidator : AbstractValidator<RestaurantUpdateDto>
{
    public RestaurantUpdateValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(255)
            .When(r => r.Name != null);

        RuleFor(r => r.Address)
            .MaximumLength(255)
            .When(r => r.Address != null);

        RuleFor(r => r.City)
            .MaximumLength(100)
            .When(r => r.City != null);

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255)
            .When(r => !string.IsNullOrEmpty(r.Email));

        RuleFor(r => r.AveragePrice)
            .GreaterThanOrEqualTo(0)
            .When(r => r.AveragePrice.HasValue);
        
        RuleFor(dto => dto)
            .Must(dto => dto.Name != null || dto.Address != null || dto.City != null || dto.Email != null || dto.AveragePrice != null).WithMessage("You must change at least one field.");
    }
}