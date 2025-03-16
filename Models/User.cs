using System.ComponentModel.DataAnnotations;
using BookRest.Models.Enums;

namespace BookRest.Models;

public class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string? PhoneNumber { get; set; }
    
    public string Password { get; set; }
    
    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public ICollection<Restaurant> OwnedRestaurants { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Review> Reviews { get; set; }
}