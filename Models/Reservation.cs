using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public int TableId { get; set; }
    public RestaurantTable Table { get; set; }

    public DateTime ReservationDateTime { get; set; }
    public int Duration { get; set; }
    public int NumberOfGuests { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; }  // 'Pending', 'Confirmed'

    public string SpecialRequests { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Notification> Notifications { get; set; }
}