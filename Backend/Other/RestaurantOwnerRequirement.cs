using System.Security.Claims;
using BookRest.Data;
using BookRest.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Other;

public class RestaurantOwnerRequirement : IAuthorizationRequirement
{
}

public class RestaurantOwnerHandler(IDbContextFactory<AppDbContext> dbContextFactory)
    : AuthorizationHandler<RestaurantOwnerRequirement, int>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RestaurantOwnerRequirement requirement,
        int restaurantId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var userId = int.Parse(
            context.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var owns = await dbContext.Restaurants
            .AnyAsync(r => r.RestaurantId == restaurantId
                           && r.OwnerId == userId);

        if (owns || context.User.IsInRole(UserRole.Admin.ToString()))
            context.Succeed(requirement);
    }
}