using AutoMapper;
using BookRest.Data;
using BookRest.Dtos.Restaurant;
using BookRest.Dtos.RestaurantTable;
using BookRest.Models;
using BookRest.Other;
using BookRest.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class RestaurantTableService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IMapper mapper,
    IValidator<RestaurantTableCreateDto> createValidator,
    IValidator<RestaurantTableUpdateDto> updateValidator) : IRestaurantTableService
{

    public async Task<OperationResult<RestaurantTableDisplayDto>> CreateTableAsync(RestaurantTableCreateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var validationResult = await createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<RestaurantTableDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var restaurantExists = await dbContext.Restaurants.AnyAsync(r => r.RestaurantId == dto.RestaurantId);
        if (!restaurantExists)
        {
            return OperationResult<RestaurantTableDisplayDto>.Fail("Restaurant does not exist.");
        }

        var table = mapper.Map<RestaurantTable>(dto);

        dbContext.RestaurantTables.Add(table);
        await dbContext.SaveChangesAsync();

        var tableDto = mapper.Map<RestaurantTableDisplayDto>(table);

        return OperationResult<RestaurantTableDisplayDto>.Ok(tableDto);
    }

    public async Task<OperationResult<RestaurantTableDisplayDto>> GetTableByIdAsync(int tableId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var table = await dbContext.RestaurantTables.FindAsync(tableId);
        if (table == null) return OperationResult<RestaurantTableDisplayDto>.Fail("Table not found.");

        var dto = mapper.Map<RestaurantTableDisplayDto>(table);

        return OperationResult<RestaurantTableDisplayDto>.Ok(dto);
    }

    public async Task<OperationResult<IEnumerable<RestaurantTableDisplayDto>>> GetTablesByRestaurantAsync(int restaurantId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var tables = await dbContext.RestaurantTables
            .Where(t => t.RestaurantId == restaurantId)
            .Select(t => mapper.Map<RestaurantTableDisplayDto>(t))
            .ToListAsync();

        return OperationResult<IEnumerable<RestaurantTableDisplayDto>>.Ok(tables);
    }

    public async Task<OperationResult<RestaurantTableDisplayDto>> UpdateTableAsync(int tableId, RestaurantTableUpdateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<RestaurantTableDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var table = await dbContext.RestaurantTables.FindAsync(tableId);
        if (table == null) return OperationResult<RestaurantTableDisplayDto>.Fail("Table not found.");

        if (!string.IsNullOrWhiteSpace(dto.TableNumber))
            table.TableNumber = dto.TableNumber;
        if (dto.SectionId.HasValue)
            table.SectionId = dto.SectionId.Value;
        if (dto.Capacity.HasValue)
            table.Capacity = dto.Capacity.Value;
        if (!string.IsNullOrWhiteSpace(dto.LocationDescription))
            table.LocationDescription = dto.LocationDescription;

        await dbContext.SaveChangesAsync();

        var tableDto = mapper.Map<RestaurantTableDisplayDto>(table);

        return OperationResult<RestaurantTableDisplayDto>.Ok(tableDto);
    }

    public async Task<OperationResult<bool>> DeleteTableAsync(int tableId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var table = await dbContext.RestaurantTables.FindAsync(tableId);
        if (table == null) return OperationResult<bool>.Fail("Table not found.");

        dbContext.RestaurantTables.Remove(table);
        await dbContext.SaveChangesAsync();

        return OperationResult<bool>.Ok(true);
    }
}