using BookRest.Models;

namespace BookRest.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();
}