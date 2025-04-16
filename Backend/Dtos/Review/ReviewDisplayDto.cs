namespace BookRest.Dtos.Review;

public class ReviewDisplayDto
{
    public int ReviewId { get; set; }
    public int RestaurantId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}