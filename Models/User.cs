using System.ComponentModel.DataAnnotations;
using BookRest.Models.Enums;

namespace BookRest.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }

    [Required]
    [MaxLength(50)]
    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public ICollection<Restaurant> OwnedRestaurants { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Review> Reviews { get; set; }
}