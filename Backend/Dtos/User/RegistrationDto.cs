namespace BookRest.Dtos.User;

public class RegistrationDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = null!;
}