using BookRest.Models.Enums;

public class UserCreateDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = default!;
    public UserRole Role { get; set; } = UserRole.Customer;
}