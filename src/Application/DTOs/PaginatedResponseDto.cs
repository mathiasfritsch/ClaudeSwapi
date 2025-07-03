namespace Application.DTOs;

public class PaginatedResponseDto<T>
{
    public string Message { get; set; } = string.Empty;
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public string? Previous { get; set; }
    public string? Next { get; set; }
    public List<T> Results { get; set; } = new();
}