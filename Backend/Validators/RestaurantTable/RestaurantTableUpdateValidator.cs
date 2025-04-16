using BookRest.Dtos.RestaurantTable;
using FluentValidation;

namespace BookRest.Validators.RestaurantTable;

public class RestaurantTableUpdateValidator : AbstractValidator<RestaurantTableUpdateDto>
{
    public RestaurantTableUpdateValidator()
    {
        RuleFor(t => t.TableNumber)
            .MaximumLength(50).When(t => !string.IsNullOrEmpty(t.TableNumber));

        RuleFor(t => t.Capacity)
            .GreaterThan(0).When(t => t.Capacity.HasValue);
    }
}