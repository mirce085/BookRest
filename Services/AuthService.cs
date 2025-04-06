using System.Security.Claims;
using BookRest.Data;
using BookRest.Dtos.Auth;
using BookRest.Dtos.User;
using BookRest.Models;
using BookRest.Other;
using BookRest.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class AuthService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    ITokenService tokenService,
    IPasswordHasher<User> passwordHasher) : IAuthService
{
    public async Task<OperationResult<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var user = await dbContext.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        var pwOk = user != null && passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password) ==
            PasswordVerificationResult.Success;

        if (!pwOk) return OperationResult<AuthResponseDto>.Fail("Invalid credentials");

        var access = tokenService.GenerateAccessToken(user!);
        var refresh = tokenService.GenerateRefreshToken();

        user!.RefreshTokens.Add(refresh);
        await dbContext.SaveChangesAsync();

        return OperationResult<AuthResponseDto>.Ok(
            new AuthResponseDto
            {
                AccessToken = access,
                RefreshToken = refresh.Token,
            });
    }

    public async Task<OperationResult<AuthResponseDto>> RefreshAsync(RefreshRequestDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var old = await dbContext.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == dto.RefreshToken);

        if (old == null || old.Expires <= DateTime.UtcNow)
            return OperationResult<AuthResponseDto>.Fail("Invalid or expired refresh token");

        dbContext.RefreshTokens.Remove(old);
        var newRefresh = tokenService.GenerateRefreshToken();
        old.User.RefreshTokens.Add(newRefresh);

        var newAccess = tokenService.GenerateAccessToken(old.User);
        await dbContext.SaveChangesAsync();

        return OperationResult<AuthResponseDto>.Ok(
            new AuthResponseDto
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh.Token
            });
    }

    public async Task<OperationResult<bool>> LogoutAsync(ClaimsPrincipal principal)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var id = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tokens = dbContext.RefreshTokens.Where(r => r.UserId == id);

        dbContext.RefreshTokens.RemoveRange(tokens);
        await dbContext.SaveChangesAsync();

        return OperationResult<bool>.Ok(true);
    }
}