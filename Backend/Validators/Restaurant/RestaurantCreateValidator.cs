using BookRest.Dtos.Restaurant;
using FluentValidation;

namespace BookRest.Validators.Restaurant;

public class RestaurantCreateValidator : AbstractValidator<RestaurantCreateDto>
{
    public RestaurantCreateValidator()
    {
        RuleFor(r => r.OwnerId)
            .GreaterThan(0).WithMessage("OwnerId must be a valid user ID.");

        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(255).WithMessage("Restaurant name cannot exceed 255 characters.");
        
        RuleFor(r => r.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(255).WithMessage("Address cannot exceed 255 characters.");

        RuleFor(r => r.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");
        
        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.")
            .When(r => !string.IsNullOrEmpty(r.Email));

        RuleFor(r => r.AveragePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Average price cannot be negative.");
    }
}