using System.Security.Claims;
using BookRest.Dtos.Auth;
using BookRest.Dtos.User;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface IAuthService
{
    Task<OperationResult<AuthResponseDto>> LoginAsync(LoginDto dto);
    Task<OperationResult<AuthResponseDto>> RefreshAsync(RefreshRequestDto dto);
    Task<OperationResult<bool>> LogoutAsync(ClaimsPrincipal user);
}