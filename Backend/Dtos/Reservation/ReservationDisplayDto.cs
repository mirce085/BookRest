namespace BookRest.Dtos.Reservation;

public class ReservationDisplayDto
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int TableId { get; set; }
    public DateTime ReservationDateTime { get; set; }
    public int Duration { get; set; }
    public int NumberOfGuests { get; set; }
    public string Status { get; set; } = null!; // 'Pending', 'Confirmed'
    public string? SpecialRequests { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}