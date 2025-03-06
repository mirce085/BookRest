using BookRest.Models.Enums;

namespace BookRest.Dtos.User;

public class UserUpdateDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public UserRole? Role { get; set; }
    public string? Password { get; set; }
}