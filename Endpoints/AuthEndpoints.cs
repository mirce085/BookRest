using System.Security.Claims;
using BookRest.Dtos.Auth;
using BookRest.Dtos.User;
using BookRest.Services.Interfaces;

namespace BookRest.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/auth");
        
        group.MapPost("/login", async (LoginDto loginDto, IAuthService authService) =>
        {
            var result = await authService.LoginAsync(loginDto);
            return result.Success
                ? Results.Ok(result.Data)
                : Results.Unauthorized(); 
        });
        
        group.MapPost("/refresh", async (RefreshRequestDto requestDto, IAuthService authService) =>
        {
            var result = await authService.RefreshAsync(requestDto);
            return result.Success
                ? Results.Ok(result.Data)
                : Results.Unauthorized(); 
        });
        
        group.MapPost("/logout", async (ClaimsPrincipal user, IAuthService authService) =>
        {
            var result = await authService.LogoutAsync(user);
            return result.Success
                ? Results.Ok(result.Data)
                : Results.Unauthorized(); 
        }).RequireAuthorization();
    }
}