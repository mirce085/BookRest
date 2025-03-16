using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class Review
{
    public int ReviewId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int Rating { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}