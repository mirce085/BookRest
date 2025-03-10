using BookRest.Dtos.User;
using FluentValidation;

namespace BookRest.Validators.User;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(u => u.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Username cannot exceed 100 characters.")
            .When(u => !string.IsNullOrEmpty(u.Email));
        
        RuleFor(u => u.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
            .MaximumLength(255).WithMessage("Password cannot exceed 255 characters.")
            .When(u => !string.IsNullOrEmpty(u.Password));
        
        RuleFor(u => u.Role)
            .IsInEnum().WithMessage("Invalid role specified.")
            .When(u => u.Role.HasValue);
        
        RuleFor(u => u.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
            .When(u => !string.IsNullOrEmpty(u.PhoneNumber));
    }
}