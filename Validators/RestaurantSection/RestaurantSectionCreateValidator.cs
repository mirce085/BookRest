using BookRest.Dtos.RestaurantSection;
using FluentValidation;

namespace BookRest.Validators.RestaurantSection;

public class RestaurantSectionCreateValidator : AbstractValidator<SectionCreateDto>
{
    public RestaurantSectionCreateValidator()
    {
        RuleFor(s => s.RestaurantId)
            .GreaterThan(0).WithMessage("RestaurantId is required.");

        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Section name is required.")
            .MaximumLength(100).WithMessage("Section name cannot exceed 100 characters.");
        
        RuleFor(s => s.Description)
            .MaximumLength(1000).WithMessage("Section name cannot exceed 1000 characters.");
    }
}