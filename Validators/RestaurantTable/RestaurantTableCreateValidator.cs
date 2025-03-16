using BookRest.Dtos.RestaurantTable;
using FluentValidation;

namespace BookRest.Validators.RestaurantTable;

public class RestaurantTableCreateValidator : AbstractValidator<RestaurantTableCreateDto>
{
    public RestaurantTableCreateValidator()
    {
        RuleFor(t => t.RestaurantId)
            .GreaterThan(0).WithMessage("RestaurantId is required.");

        RuleFor(t => t.TableNumber)
            .NotEmpty().WithMessage("TableNumber is required.")
            .MaximumLength(50).WithMessage("TableNumber cannot exceed 50 characters.");

        RuleFor(t => t.Capacity)
            .GreaterThan(0).WithMessage("Table capacity must be greater than 0.");
    }
}