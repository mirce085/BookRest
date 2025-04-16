using BookRest.Dtos.MenuItem;
using FluentValidation;

namespace BookRest.Validators.MenuItem;

public class MenuItemUpdateValidator : AbstractValidator<MenuItemUpdateDto>
{
    public MenuItemUpdateValidator()
    {
        RuleFor(x => x)
            .Must(dto => dto.Name != null || dto.Description != null || dto.Price.HasValue || dto.Category != null)
            .WithMessage("At least one field must be provided for update.");

        RuleFor(x => x.Name!)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => x.Name != null);
        
        RuleFor(x => x.Description!)
            .MaximumLength(1000)
            .When(x => x.Description != null);

        RuleFor(x => x.Price!.Value)
            .GreaterThan(0).When(x => x.Price.HasValue);

        RuleFor(x => x.Category!)
            .MaximumLength(100).When(x => x.Category != null);
    }
}