using BookRest.Dtos.User;
using BookRest.Services.Interfaces;

namespace BookRest.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users");
        
        group.MapGet("/", async (IUserService userService) =>
        {
            var users = await userService.GetAllUsersAsync();
            return Results.Ok(users);
        });
        
        group.MapGet("/{id:int}", async (int id, IUserService userService) =>
        {
            var user = await userService.GetUserByIdAsync(id);
            return Results.Ok(user);
        });
        
        group.MapPost("/", async (UserCreateDto dto, IUserService userService) =>
        {
            var createdUser = await userService.CreateUserAsync(dto);
            return Results.Created($"/api/users/{createdUser.Data!.UserId}", createdUser);
        }).RequireAuthorization("AdminOnly");
        
        group.MapPut("/{id:int}", async (int id, UserUpdateDto dto, IUserService userService) =>
        {
            var updatedUser = await userService.UpdateUserAsync(id, dto);
            return Results.Ok(updatedUser);
        }).RequireAuthorization("AdminOnly");
        
        group.MapDelete("/{id:int}", async (int id, IUserService userService) =>
        {
            var success = await userService.DeleteUserAsync(id);
            return success.Data ? Results.NoContent() : Results.NotFound();
        }).RequireAuthorization("AdminOnly");;
    }
}