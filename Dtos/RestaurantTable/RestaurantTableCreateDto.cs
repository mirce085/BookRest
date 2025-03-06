namespace BookRest.Dtos.RestaurantTable;

public class RestaurantTableCreateDto
{
    public int RestaurantId { get; set; }
    public int? SectionId { get; set; }
    public string TableNumber { get; set; } = null!;
    public int Capacity { get; set; }
    public string? LocationDescription { get; set; }
}