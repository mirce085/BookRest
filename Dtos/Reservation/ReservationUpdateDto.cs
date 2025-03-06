namespace BookRest.Dtos.Reservation;

public class ReservationUpdateDto
{
    public DateTime? ReservationDateTime { get; set; }
    public int? Duration { get; set; }
    public int? NumberOfGuests { get; set; }
    public string? Status { get; set; }
    public string? SpecialRequests { get; set; }
}