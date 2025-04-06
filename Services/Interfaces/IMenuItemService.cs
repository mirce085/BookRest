using BookRest.Dtos.MenuItem;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface IMenuItemService
{
    Task<OperationResult<MenuItemDisplayDto>> CreateMenuItemAsync(MenuItemCreateDto dto);
    Task<OperationResult<MenuItemDisplayDto>> GetMenuItemByIdAsync(int menuItemId);
    Task<OperationResult<IEnumerable<MenuItemDisplayDto>>> GetMenuItemsByRestaurantAsync(int restaurantId);
    Task<OperationResult<MenuItemDisplayDto>> UpdateMenuItemAsync(int menuItemId, MenuItemUpdateDto dto);
    Task<OperationResult<bool>> DeleteMenuItemAsync(int menuItemId);
}