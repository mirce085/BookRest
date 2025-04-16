using BookRest.Dtos.User;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface IUserService
{
    Task<OperationResult<UserDisplayDto>> GetUserByIdAsync(int userId);
    Task<OperationResult<IEnumerable<UserDisplayDto>>> GetAllUsersAsync();
    Task<OperationResult<UserDisplayDto>> CreateUserAsync(UserCreateDto dto);
    Task<OperationResult<UserDisplayDto>> UpdateUserAsync(int userId, UserUpdateDto dto);
    Task<OperationResult<bool>> DeleteUserAsync(int userId);
    Task<OperationResult<UserDisplayDto>> RegisterUserAsync(RegistrationDto dto);
}