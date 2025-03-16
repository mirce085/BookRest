using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; }
    
    public string Description { get; set; }

    public virtual ICollection<Restaurant> Restaurants { get; set; }
}