using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PlanetService : IPlanetService
{
    private readonly ISwapiClient _swapiClient;
    private readonly ILogger<PlanetService> _logger;

    public PlanetService(ISwapiClient swapiClient, ILogger<PlanetService> logger)
    {
        _swapiClient = swapiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponseDto<PlanetSummaryDto>> GetPlanetsAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _swapiClient.GetPlanetsAsync(page, limit);
            
            if (response == null)
            {
                _logger.LogWarning("SWAPI returned null response for planets request");
                return new PaginatedResponseDto<PlanetSummaryDto>();
            }

            return new PaginatedResponseDto<PlanetSummaryDto>
            {
                Message = response.Message,
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Previous = response.Previous,
                Next = response.Next,
                Results = response.Results.Select(MapToPlanetSummaryDto).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching planets from SWAPI");
            throw;
        }
    }

    public async Task<PlanetDto?> GetPlanetAsync(string id)
    {
        try
        {
            var planet = await _swapiClient.GetPlanetAsync(id);
            
            if (planet == null)
            {
                _logger.LogWarning("SWAPI returned null for planet with ID: {PlanetId}", id);
                return null;
            }

            return MapToPlanetDto(planet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching planet {PlanetId} from SWAPI", id);
            throw;
        }
    }

    private static PlanetSummaryDto MapToPlanetSummaryDto(Planet planet)
    {
        return new PlanetSummaryDto
        {
            Uid = planet.Uid,
            Name = planet.Name,
            Url = planet.Url
        };
    }

    private static PlanetDto MapToPlanetDto(Planet planet)
    {
        return new PlanetDto
        {
            Uid = planet.Uid,
            Name = planet.Name,
            RotationPeriod = planet.RotationPeriod,
            OrbitalPeriod = planet.OrbitalPeriod,
            Diameter = planet.Diameter,
            Climate = planet.Climate,
            Gravity = planet.Gravity,
            Terrain = planet.Terrain,
            SurfaceWater = planet.SurfaceWater,
            Population = planet.Population,
            Residents = planet.Residents,
            Films = planet.Films,
            Created = planet.Created,
            Edited = planet.Edited,
            Url = planet.Url
        };
    }
}