using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRest.Models;

public class MenuItem
{
    public int MenuItemId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string Category { get; set; }
}