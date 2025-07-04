using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class StarshipService : IStarshipService
{
    private readonly ISwapiClient _swapiClient;
    private readonly ILogger<StarshipService> _logger;

    public StarshipService(ISwapiClient swapiClient, ILogger<StarshipService> logger)
    {
        _swapiClient = swapiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponseDto<StarshipSummaryDto>> GetStarshipsAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _swapiClient.GetStarshipsAsync(page, limit);
            
            if (response == null)
            {
                _logger.LogWarning("SWAPI returned null response for starships request");
                return new PaginatedResponseDto<StarshipSummaryDto>();
            }

            return new PaginatedResponseDto<StarshipSummaryDto>
            {
                Message = response.Message,
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Previous = response.Previous,
                Next = response.Next,
                Results = response.Results.Select(MapToStarshipSummaryDto).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching starships from SWAPI");
            throw;
        }
    }

    public async Task<StarshipDto?> GetStarshipAsync(string id)
    {
        try
        {
            var starship = await _swapiClient.GetStarshipAsync(id);
            
            if (starship == null)
            {
                _logger.LogWarning("SWAPI returned null for starship with ID: {StarshipId}", id);
                return null;
            }

            return MapToStarshipDto(starship);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching starship {StarshipId} from SWAPI", id);
            throw;
        }
    }

    private static StarshipSummaryDto MapToStarshipSummaryDto(Starship starship)
    {
        return new StarshipSummaryDto
        {
            Uid = starship.Uid,
            Name = starship.Name,
            Url = starship.Url
        };
    }

    private static StarshipDto MapToStarshipDto(Starship starship)
    {
        return new StarshipDto
        {
            Uid = starship.Uid,
            Name = starship.Name,
            Model = starship.Model,
            Manufacturer = starship.Manufacturer,
            CostInCredits = starship.CostInCredits,
            Length = starship.Length,
            MaxAtmospheringSpeed = starship.MaxAtmospheringSpeed,
            Crew = starship.Crew,
            Passengers = starship.Passengers,
            CargoCapacity = starship.CargoCapacity,
            Consumables = starship.Consumables,
            HyperdriveRating = starship.HyperdriveRating,
            MGLT = starship.MGLT,
            StarshipClass = starship.StarshipClass,
            Pilots = starship.Pilots,
            Films = starship.Films,
            Created = starship.Created,
            Edited = starship.Edited,
            Url = starship.Url
        };
    }
}