namespace Application.DTOs;

public class PersonDto
{
    public string Uid { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? SkinColor { get; set; }
    public string? HairColor { get; set; }
    public string? Height { get; set; }
    public string? EyeColor { get; set; }
    public string? Mass { get; set; }
    public string? BirthYear { get; set; }
    public string? Homeworld { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
    public string Url { get; set; } = string.Empty;
}