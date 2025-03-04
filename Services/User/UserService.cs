using BookRest.Dtos.User;
using BookRest.Extensions;
using BookRest.Other;

namespace BookRest.Services.User;

public class UserService : IUserService
{
    public Task<OperationResult<UserDisplayDto>> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<IEnumerable<UserDisplayDto>>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<UserDisplayDto>> CreateUserAsync(UserCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<UserDisplayDto>> UpdateUserAsync(int userId, UserUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<bool>> DeleteUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
}