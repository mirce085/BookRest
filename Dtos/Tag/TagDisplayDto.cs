namespace BookRest.Dtos.Tag;

public class TagDisplayDto
{
    public int TagId { get; set; }
    public string TagName { get; set; } = null!;
    public string? Description { get; set; }
}