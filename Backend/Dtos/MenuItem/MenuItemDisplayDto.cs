namespace BookRest.Dtos.MenuItem;

public class MenuItemDisplayDto
{
    public int MenuItemId { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
}