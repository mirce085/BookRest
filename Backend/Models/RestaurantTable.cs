using System.ComponentModel.DataAnnotations;

namespace BookRest.Models;

public class RestaurantTable
{
    public int TableId { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public int? SectionId { get; set; }
    public RestaurantSection Section { get; set; }
    
    public string TableNumber { get; set; }

    public int Capacity { get; set; }
    
    public string LocationDescription { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
}