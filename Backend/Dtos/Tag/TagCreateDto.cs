namespace BookRest.Dtos.Tag;

public class TagCreateDto
{
    public string TagName { get; set; } = null!;
    public string? Description { get; set; }
}