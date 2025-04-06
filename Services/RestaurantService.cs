using AutoMapper;
using BookRest.Data;
using BookRest.Dtos.Restaurant;
using BookRest.Dtos.User;
using BookRest.Models;
using BookRest.Models.Enums;
using BookRest.Other;
using BookRest.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class RestaurantService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IMapper mapper,
    IValidator<RestaurantCreateDto> createValidator,
    IValidator<RestaurantUpdateDto> updateValidator) : IRestaurantService
{
    public async Task<OperationResult<RestaurantDisplayDto>> GetRestaurantByIdAsync(int restaurantId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var restaurant = await dbContext.Restaurants.FindAsync(restaurantId);
        if (restaurant == null)
        {
            return OperationResult<RestaurantDisplayDto>.Fail("Restaurant not found.");
        }

        var userDto = mapper.Map<RestaurantDisplayDto>(restaurant);

        return OperationResult<RestaurantDisplayDto>.Ok(userDto);
    }

    public async Task<OperationResult<IEnumerable<RestaurantDisplayDto>>> GetAllRestaurantsAsync()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var restaurants = await dbContext.Restaurants
            .Select(r => mapper.Map<RestaurantDisplayDto>(r))
            .ToListAsync();

        return OperationResult<IEnumerable<RestaurantDisplayDto>>.Ok(restaurants);
    }

    public async Task<OperationResult<RestaurantDisplayDto>> CreateRestaurantAsync(RestaurantCreateDto dto)
    {
        var validationResult = await createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<RestaurantDisplayDto>.Fail(errors);
        }

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        bool ownerExists = await dbContext.Users.AnyAsync(u => u.UserId == dto.OwnerId && u.Role == UserRole.Owner);
        if (!ownerExists)
            return OperationResult<RestaurantDisplayDto>.Fail("Owner not found or user is not an Owner.");

        var restaurant = mapper.Map<Restaurant>(dto);

        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();

        var createdDto = mapper.Map<RestaurantDisplayDto>(restaurant);

        return OperationResult<RestaurantDisplayDto>.Ok(createdDto);
    }

    public async Task<OperationResult<RestaurantDisplayDto>> UpdateRestaurantAsync(int restaurantId,
        RestaurantUpdateDto dto)
    {
        var validationResult = await updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<RestaurantDisplayDto>.Fail(errors);
        }

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var restaurant = await dbContext.Restaurants.FindAsync(restaurantId);
        if (restaurant == null)
            return OperationResult<RestaurantDisplayDto>.Fail("Restaurant not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name)) restaurant.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.Description)) restaurant.Description = dto.Description;
        if (!string.IsNullOrWhiteSpace(dto.Address)) restaurant.Address = dto.Address;
        if (!string.IsNullOrWhiteSpace(dto.City)) restaurant.City = dto.City;
        if (!string.IsNullOrWhiteSpace(dto.State)) restaurant.State = dto.State;
        if (!string.IsNullOrWhiteSpace(dto.Country)) restaurant.Country = dto.Country;
        if (!string.IsNullOrWhiteSpace(dto.Zip)) restaurant.Zip = dto.Zip;
        if (!string.IsNullOrWhiteSpace(dto.Phone)) restaurant.Phone = dto.Phone;
        if (!string.IsNullOrWhiteSpace(dto.Email)) restaurant.Email = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.Website)) restaurant.Website = dto.Website;
        if (dto.AveragePrice.HasValue) restaurant.AveragePrice = dto.AveragePrice.Value;

        restaurant.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        var updatedDto = mapper.Map<RestaurantDisplayDto>(restaurant);

        return OperationResult<RestaurantDisplayDto>.Ok(updatedDto);
    }

    public async Task<OperationResult<bool>> DeleteRestaurantAsync(int restaurantId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var restaurant = await dbContext.Restaurants.FindAsync(restaurantId);
        if (restaurant == null)
            return OperationResult<bool>.Fail("Restaurant not found.");

        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();

        return OperationResult<bool>.Ok(true);
    }

    public async Task<OperationResult<IEnumerable<RestaurantDisplayDto>>> GetRestaurantsByCityAsync(string city)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var result = await dbContext.Restaurants
            .Where(r => r.City == city)
            .Select(r => mapper.Map<RestaurantDisplayDto>(r))
            .ToListAsync();

        return OperationResult<IEnumerable<RestaurantDisplayDto>>.Ok(result);
    }

    public async Task<OperationResult<IEnumerable<RestaurantDisplayDto>>> SearchRestaurantsAsync(string? city,
        string? tag)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var result1 = await dbContext.Restaurants
            .Where(r => city != null && r.City.ToLower().Contains(city.ToLower()))
            .Select(r => mapper.Map<RestaurantDisplayDto>(r))
            .ToListAsync();

        var result2 = await dbContext.Restaurants
            .Where(r => r.Tags.Any(rt =>
                tag != null &&
                rt.TagName.ToLower().Contains(tag.ToLower())))
            .Select(r => mapper.Map<RestaurantDisplayDto>(r)).ToListAsync();

        return OperationResult<IEnumerable<RestaurantDisplayDto>>.Ok(result1.Union(result2));
    }
}