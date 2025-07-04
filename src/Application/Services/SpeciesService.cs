using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class SpeciesService : ISpeciesService
{
    private readonly ISwapiClient _swapiClient;
    private readonly ILogger<SpeciesService> _logger;

    public SpeciesService(ISwapiClient swapiClient, ILogger<SpeciesService> logger)
    {
        _swapiClient = swapiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponseDto<SpeciesSummaryDto>> GetSpeciesAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _swapiClient.GetSpeciesAsync(page, limit);
            
            if (response == null)
            {
                _logger.LogWarning("SWAPI returned null response for species request");
                return new PaginatedResponseDto<SpeciesSummaryDto>();
            }

            return new PaginatedResponseDto<SpeciesSummaryDto>
            {
                Message = response.Message,
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Previous = response.Previous,
                Next = response.Next,
                Results = response.Results.Select(MapToSpeciesSummaryDto).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching species from SWAPI");
            throw;
        }
    }

    public async Task<SpeciesDto?> GetSpeciesAsync(string id)
    {
        try
        {
            var species = await _swapiClient.GetSpeciesDetailAsync(id);
            
            if (species == null)
            {
                _logger.LogWarning("SWAPI returned null for species with ID: {SpeciesId}", id);
                return null;
            }

            return MapToSpeciesDto(species);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching species {SpeciesId} from SWAPI", id);
            throw;
        }
    }

    private static SpeciesSummaryDto MapToSpeciesSummaryDto(Species species)
    {
        return new SpeciesSummaryDto
        {
            Uid = species.Uid,
            Name = species.Name,
            Url = species.Url
        };
    }

    private static SpeciesDto MapToSpeciesDto(Species species)
    {
        return new SpeciesDto
        {
            Uid = species.Uid,
            Name = species.Name,
            Classification = species.Classification,
            Designation = species.Designation,
            AverageHeight = species.AverageHeight,
            SkinColors = species.SkinColors,
            HairColors = species.HairColors,
            EyeColors = species.EyeColors,
            AverageLifespan = species.AverageLifespan,
            Homeworld = species.Homeworld,
            Language = species.Language,
            People = species.People,
            Films = species.Films,
            Created = species.Created,
            Edited = species.Edited,
            Url = species.Url
        };
    }
}