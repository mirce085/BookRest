namespace BookRest.Dtos.RestaurantSection;

public class SectionDisplayDto
{
    public int SectionId { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}