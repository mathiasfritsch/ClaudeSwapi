using Application.DTOs;

namespace Application.Interfaces;

public interface IPlanetService
{
    Task<PaginatedResponseDto<PlanetSummaryDto>> GetPlanetsAsync(int? page = null, int? limit = null);
    Task<PlanetDto?> GetPlanetAsync(string id);
}