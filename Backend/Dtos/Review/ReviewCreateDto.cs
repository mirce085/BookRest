namespace BookRest.Dtos.Review;

public class ReviewCreateDto
{
    public int RestaurantId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}