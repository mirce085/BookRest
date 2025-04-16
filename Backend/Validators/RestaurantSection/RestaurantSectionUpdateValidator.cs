using BookRest.Dtos.RestaurantSection;
using FluentValidation;

namespace BookRest.Validators.RestaurantSection;

public class RestaurantSectionUpdateValidator : AbstractValidator<SectionUpdateDto>
{
    public RestaurantSectionUpdateValidator()
    {
        RuleFor(s => s.Name)
            .MaximumLength(100)
            .When(s => s.Name != null);
        
        RuleFor(s => s.Description)
            .MaximumLength(1000).WithMessage("Section name cannot exceed 1000 characters.")
            .When(s => s.Description != null);
        
        RuleFor(dto => dto)
            .Must(dto => dto.Name != null || dto.Description != null).WithMessage("You must change at least one field.");
    }
}