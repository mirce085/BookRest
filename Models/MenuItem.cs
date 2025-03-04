using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRest.Models;

public class MenuItem
{
    [Key]
    public int MenuItemId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [MaxLength(100)]
    public string Category { get; set; }
}