using Application.DTOs;

namespace Application.Interfaces;

public interface ISpeciesService
{
    Task<PaginatedResponseDto<SpeciesSummaryDto>> GetSpeciesAsync(int? page = null, int? limit = null);
    Task<SpeciesDto?> GetSpeciesAsync(string id);
}