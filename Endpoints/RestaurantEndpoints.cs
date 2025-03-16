using BookRest.Dtos.Restaurant;
using BookRest.Services.Interfaces;

namespace BookRest.Endpoints;

public static class RestaurantEndpoints
{
    public static void MapRestaurantEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/restaurants");
        
        group.MapGet("/", async (IRestaurantService restaurantService) =>
        {
            var restaurants = await restaurantService.GetAllRestaurantsAsync();
            return Results.Ok(restaurants);
        });
        
        group.MapGet("/{id:int}", async (int id, IRestaurantService restaurantService) =>
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);
            return Results.Ok(restaurant);
        });

        group.MapPost("/", async (RestaurantCreateDto dto, IRestaurantService restaurantService) =>
        {
            var createdRestaurant = await restaurantService.CreateRestaurantAsync(dto);
            return Results.Created($"/api/restaurants/{createdRestaurant.Data!.RestaurantId}", createdRestaurant);
        });
        
        group.MapPut("/{id:int}", async (int id, RestaurantUpdateDto dto, IRestaurantService restaurantService) =>
        {
            var updatedRestaurant = await restaurantService.UpdateRestaurantAsync(id, dto);
            return Results.Ok(updatedRestaurant);
        });

        group.MapDelete("/{id:int}", async (int id, IRestaurantService restaurantService) =>
        {
            var success = await restaurantService.DeleteRestaurantAsync(id);
            return success.Data ? Results.NoContent() : Results.NotFound();
        });

        group.MapGet("/search", async (string? city, string? tag, IRestaurantService restaurantService) =>
        {
            var results = await restaurantService.SearchRestaurantsAsync(city, tag);
            return Results.Ok(results);
        });
    }
}
