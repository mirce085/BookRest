namespace BookRest.Dtos.Reservation;

public class ReservationCreateDto
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int TableId { get; set; }
    public DateTime ReservationDateTime { get; set; }
    public int Duration { get; set; }
    public int NumberOfGuests { get; set; }
    public string? SpecialRequests { get; set; }

}