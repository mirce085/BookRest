using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class RestaurantTable
{
    [Key]
    public int TableId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public int? SectionId { get; set; }
    public RestaurantSection Section { get; set; }

    [Required]
    [MaxLength(50)]
    public string TableNumber { get; set; }

    public int Capacity { get; set; }

    [MaxLength(255)]
    public string LocationDescription { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
}