using AutoMapper;
using BookRest.Data;
using BookRest.Dtos.MenuItem;
using BookRest.Dtos.Reservation;
using BookRest.Hubs;
using BookRest.Models;
using BookRest.Other;
using BookRest.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class MenuItemService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IMapper mapper,
    IValidator<MenuItemCreateDto> createValidator,
    IValidator<MenuItemUpdateDto> updateValidator) : IMenuItemService
{

    public async Task<OperationResult<MenuItemDisplayDto>> CreateMenuItemAsync(MenuItemCreateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<MenuItemDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }
        
        bool restaurantExists = await dbContext.Restaurants.AnyAsync(r => r.RestaurantId == dto.RestaurantId);
        if (!restaurantExists)
            return OperationResult<MenuItemDisplayDto>.Fail("Restaurant does not exist.");

        var menuItem = mapper.Map<MenuItem>(dto);

        dbContext.MenuItems.Add(menuItem);
        await dbContext.SaveChangesAsync();

        var resultDto = mapper.Map<MenuItemDisplayDto>(menuItem);

        return OperationResult<MenuItemDisplayDto>.Ok(resultDto);
    }

    public async Task<OperationResult<MenuItemDisplayDto>> GetMenuItemByIdAsync(int menuItemId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var item = await dbContext.MenuItems.FindAsync(menuItemId);
        if (item == null)
            return OperationResult<MenuItemDisplayDto>.Fail("Menu item not found.");

        var dto = mapper.Map<MenuItemDisplayDto>(item);

        return OperationResult<MenuItemDisplayDto>.Ok(dto);
    }

    public async Task<OperationResult<IEnumerable<MenuItemDisplayDto>>> GetMenuItemsByRestaurantAsync(int restaurantId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var items = await dbContext.MenuItems
            .Where(m => m.RestaurantId == restaurantId)
            .Select(m => mapper.Map<MenuItemDisplayDto>(m))
            .ToListAsync();

        return OperationResult<IEnumerable<MenuItemDisplayDto>>.Ok(items);
    }

    public async Task<OperationResult<MenuItemDisplayDto>> UpdateMenuItemAsync(int menuItemId, MenuItemUpdateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<MenuItemDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var item = await dbContext.MenuItems.FindAsync(menuItemId);
        if (item == null)
            return OperationResult<MenuItemDisplayDto>.Fail("Menu item not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name)) item.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.Description)) item.Description = dto.Description;
        if (dto.Price.HasValue) item.Price = dto.Price.Value;
        if (!string.IsNullOrWhiteSpace(dto.Category)) item.Category = dto.Category;

        await dbContext.SaveChangesAsync();

        var updatedDto = mapper.Map<MenuItemDisplayDto>(item);

        return OperationResult<MenuItemDisplayDto>.Ok(updatedDto);
    }

    public async Task<OperationResult<bool>> DeleteMenuItemAsync(int menuItemId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var item = await dbContext.MenuItems.FindAsync(menuItemId);
        if (item == null)
            return OperationResult<bool>.Fail("Menu item not found.");

        dbContext.MenuItems.Remove(item);
        await dbContext.SaveChangesAsync();

        return OperationResult<bool>.Ok(true);
    }
}