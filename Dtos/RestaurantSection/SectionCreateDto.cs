namespace BookRest.Dtos.RestaurantSection;

public class SectionCreateDto
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}