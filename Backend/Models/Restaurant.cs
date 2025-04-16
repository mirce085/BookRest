using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRest.Models;

public class Restaurant
{
    public int RestaurantId { get; set; }
    
    public int OwnerId { get; set; }
    public User Owner { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
    
    public string Address { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string Country { get; set; }

    public string Zip { get; set; }
    
    public string Phone { get; set; }
    
    public string Email { get; set; }

    public string Website { get; set; }

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