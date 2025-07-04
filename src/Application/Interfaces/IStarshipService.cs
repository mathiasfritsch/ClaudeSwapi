using Application.DTOs;

namespace Application.Interfaces;

public interface IStarshipService
{
    Task<PaginatedResponseDto<StarshipSummaryDto>> GetStarshipsAsync(int? page = null, int? limit = null);
    Task<StarshipDto?> GetStarshipAsync(string id);
}