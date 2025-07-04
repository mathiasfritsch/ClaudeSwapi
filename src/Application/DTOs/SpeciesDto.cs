namespace Application.DTOs;

public class SpeciesDto
{
    public string Uid { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Classification { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string AverageHeight { get; set; } = string.Empty;
    public string SkinColors { get; set; } = string.Empty;
    public string HairColors { get; set; } = string.Empty;
    public string EyeColors { get; set; } = string.Empty;
    public string AverageLifespan { get; set; } = string.Empty;
    public string Homeworld { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public List<string> People { get; set; } = new();
    public List<string> Films { get; set; } = new();
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
    public string Url { get; set; } = string.Empty;
}

public class SpeciesSummaryDto
{
    public string Uid { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}