using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly IPlanetService _planetService;
    private readonly ILogger<PlanetsController> _logger;

    public PlanetsController(IPlanetService planetService, ILogger<PlanetsController> logger)
    {
        _planetService = planetService;
        _logger = logger;
    }

    /// <summary>
    /// Get a paginated list of planets from SWAPI
    /// </summary>
    /// <param name="page">Page number (optional)</param>
    /// <param name="limit">Number of items per page (optional)</param>
    /// <returns>Paginated list of planets</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponseDto<PlanetSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponseDto<PlanetSummaryDto>>> GetPlanets(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            _logger.LogInformation("Getting planets with page: {Page}, limit: {Limit}", page, limit);
            
            var result = await _planetService.GetPlanetsAsync(page, limit);
            
            _logger.LogInformation("Successfully retrieved {Count} planets", result.Results.Count);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting planets");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving planets data");
        }
    }

    /// <summary>
    /// Get a specific planet by ID from SWAPI
    /// </summary>
    /// <param name="id">Planet ID</param>
    /// <returns>Planet details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlanetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PlanetDto>> GetPlanet(string id)
    {
        try
        {
            _logger.LogInformation("Getting planet with ID: {PlanetId}", id);
            
            var planet = await _planetService.GetPlanetAsync(id);
            
            if (planet == null)
            {
                _logger.LogWarning("Planet with ID {PlanetId} not found", id);
                return NotFound($"Planet with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved planet: {PlanetName}", planet.Name);
            
            return Ok(planet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting planet {PlanetId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving planet data");
        }
    }
}