using AutoMapper;
using BookRest.Data;
using BookRest.Dtos.User;
using BookRest.Extensions;
using BookRest.Models;
using BookRest.Other;
using BookRest.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class UserService(IDbContextFactory<AppDbContext> dbContextFactory, IMapper mapper) : IUserService
{
    public async Task<OperationResult<UserDisplayDto>> GetUserByIdAsync(int userId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var user = await dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            return OperationResult<UserDisplayDto>.Fail("User not found.");
        }
        
        var userDto = mapper.Map<UserDisplayDto>(user);

        return OperationResult<UserDisplayDto>.Ok(userDto);
    }

    public async Task<OperationResult<IEnumerable<UserDisplayDto>>> GetAllUsersAsync()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var users = await dbContext.Users
            .Select(u => mapper.Map<UserDisplayDto>(u))
            .ToListAsync();

        return OperationResult<IEnumerable<UserDisplayDto>>.Ok(users);
    }

    public async Task<OperationResult<UserDisplayDto>> CreateUserAsync(UserCreateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var emailExists = await dbContext.Users.AnyAsync(u => u.Email == dto.Email);
        if (emailExists)
        {
            return OperationResult<UserDisplayDto>.Fail("Email already in use.");
        }

        dto.Password = dto.Password.HashString();
        
        var user = mapper.Map<User>(dto);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        var userDto = mapper.Map<UserDisplayDto>(user);

        return OperationResult<UserDisplayDto>.Ok(userDto);
    }

    public async Task<OperationResult<UserDisplayDto>> UpdateUserAsync(int userId, UserUpdateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var user = await dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            return OperationResult<UserDisplayDto>.Fail("User not found.");
        }
        
        if (!string.IsNullOrWhiteSpace(dto.Username))
            user.Username = dto.Username;

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            bool emailExists = await dbContext.Users
                .AnyAsync(u => u.Email == dto.Email && u.UserId != userId);
            if (emailExists) return OperationResult<UserDisplayDto>.Fail("Email already in use.");

            user.Email = dto.Email;
        }

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            user.PhoneNumber = dto.PhoneNumber;

        if (user.Role != dto.Role && dto.Role is not null)
            user.Role = dto.Role.Value;

        if (!string.IsNullOrWhiteSpace(dto.Password))
            dto.Password = dto.Password.HashString();

        await dbContext.SaveChangesAsync();

        var userDto = mapper.Map<UserDisplayDto>(user);

        return OperationResult<UserDisplayDto>.Ok(userDto);
    }

    public async Task<OperationResult<bool>> DeleteUserAsync(int userId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var user = await dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            return OperationResult<bool>.Fail("User not found.");
        }

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();

        return OperationResult<bool>.Ok(true);
    }
}