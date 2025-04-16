namespace BookRest.Dtos.RestaurantTable;

public class RestaurantTableUpdateDto
{
    public int? SectionId { get; set; }
    public string? TableNumber { get; set; }
    public int? Capacity { get; set; }
    public string? LocationDescription { get; set; }
}