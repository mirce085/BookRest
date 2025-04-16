using BookRest.Models.Enums;

namespace BookRest.Dtos.User;

public class UserCreateDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }
}