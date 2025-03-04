using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRest.Models;

public class RestaurantSection
{
    [Key]
    public int SectionId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<RestaurantTable> Tables { get; set; }
}