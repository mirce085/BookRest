using BookRest.Dtos.RestaurantTable;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface IRestaurantTableService
{
    Task<OperationResult<RestaurantTableDisplayDto>> CreateTableAsync(RestaurantTableCreateDto dto);
    Task<OperationResult<RestaurantTableDisplayDto>> GetTableByIdAsync(int tableId);
    Task<OperationResult<IEnumerable<RestaurantTableDisplayDto>>> GetTablesByRestaurantAsync(int restaurantId);
    Task<OperationResult<RestaurantTableDisplayDto>> UpdateTableAsync(int tableId, RestaurantTableUpdateDto dto);
    Task<OperationResult<bool>> DeleteTableAsync(int tableId);
}