using BookRest.Dtos.Restaurant;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface IRestaurantService
{
    Task<OperationResult<RestaurantDisplayDto>> GetRestaurantByIdAsync(int restaurantId);
    Task<OperationResult<IEnumerable<RestaurantDisplayDto>>> GetAllRestaurantsAsync();
    Task<OperationResult<RestaurantDisplayDto>> CreateRestaurantAsync(RestaurantCreateDto dto);
    Task<OperationResult<RestaurantDisplayDto>> UpdateRestaurantAsync(int restaurantId, RestaurantUpdateDto dto);
    Task<OperationResult<bool>> DeleteRestaurantAsync(int restaurantId);
    Task<OperationResult<IEnumerable<RestaurantDisplayDto>>> GetRestaurantsByCityAsync(string city);
    Task<OperationResult<IEnumerable<RestaurantDisplayDto>>> SearchRestaurantsAsync(string? city, string? tag);
}