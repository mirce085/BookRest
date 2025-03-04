namespace BookRest.Dtos.User;

public class UserDisplayDto
{
    public int UserID { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}