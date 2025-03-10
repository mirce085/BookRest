using BookRest.Dtos.Restaurant;
using FluentValidation;

namespace BookRest.Validators.Restaurant;

public class RestaurantCreateValidator : AbstractValidator<RestaurantCreateDto>
{
    public RestaurantCreateValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

        RuleFor(r => r.AveragePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Average price cannot be negative.");
    }
}