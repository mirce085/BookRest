using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRest.Models;

public class Restaurant
{
    [Key]
    public int RestaurantId { get; set; }
    
    public int OwnerId { get; set; }
    public User Owner { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string Description { get; set; }

    [MaxLength(255)]
    public string Address { get; set; }

    [MaxLength(100)]
    public string City { get; set; }

    [MaxLength(100)]
    public string State { get; set; }

    [MaxLength(100)]
    public string Country { get; set; }

    [MaxLength(20)]
    public string Zip { get; set; }

    [MaxLength(20)]
    public string Phone { get; set; }

    [MaxLength(255)]
    public string Email { get; set; }

    [MaxLength(255)]
    public string Website { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? AveragePrice { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<RestaurantSection> Sections { get; set; }
    public ICollection<RestaurantTable> Tables { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<OperatingHour> OperatingHours { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; }
    public ICollection<Review> Reviews { get; set; }
    
    public virtual ICollection<Tag> Tags { get; set; }
}