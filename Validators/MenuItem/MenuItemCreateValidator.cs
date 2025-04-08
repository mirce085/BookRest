using BookRest.Dtos.MenuItem;
using FluentValidation;

namespace BookRest.Validators.MenuItem;

public class MenuItemCreateValidator : AbstractValidator<MenuItemCreateDto>
{
    public MenuItemCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Category)
            .MaximumLength(100);
    }
}