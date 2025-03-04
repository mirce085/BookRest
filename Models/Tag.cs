using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class Tag
{
    [Key]
    public int TagId { get; set; }

    [Required]
    [MaxLength(100)]
    public string TagName { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    public virtual ICollection<Restaurant> Restaurants { get; set; }
}