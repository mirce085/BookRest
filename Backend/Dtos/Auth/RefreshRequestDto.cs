namespace BookRest.Dtos.Auth;

public class RefreshRequestDto
{
    public string RefreshToken { get; init; } = null!;
}