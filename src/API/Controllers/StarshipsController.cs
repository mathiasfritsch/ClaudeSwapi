using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StarshipsController : ControllerBase
{
    private readonly IStarshipService _starshipService;
    private readonly ILogger<StarshipsController> _logger;

    public StarshipsController(IStarshipService starshipService, ILogger<StarshipsController> logger)
    {
        _starshipService = starshipService;
        _logger = logger;
    }

    /// <summary>
    /// Get a paginated list of starships from SWAPI
    /// </summary>
    /// <param name="page">Page number (optional)</param>
    /// <param name="limit">Number of items per page (optional)</param>
    /// <returns>Paginated list of starships</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponseDto<StarshipSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponseDto<StarshipSummaryDto>>> GetStarships(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            _logger.LogInformation("Getting starships with page: {Page}, limit: {Limit}", page, limit);
            
            var result = await _starshipService.GetStarshipsAsync(page, limit);
            
            _logger.LogInformation("Successfully retrieved {Count} starships", result.Results.Count);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting starships");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving starships data");
        }
    }

    /// <summary>
    /// Get a specific starship by ID from SWAPI
    /// </summary>
    /// <param name="id">Starship ID</param>
    /// <returns>Starship details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StarshipDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StarshipDto>> GetStarship(string id)
    {
        try
        {
            _logger.LogInformation("Getting starship with ID: {StarshipId}", id);
            
            var starship = await _starshipService.GetStarshipAsync(id);
            
            if (starship == null)
            {
                _logger.LogWarning("Starship with ID {StarshipId} not found", id);
                return NotFound($"Starship with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved starship: {StarshipName}", starship.Name);
            
            return Ok(starship);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting starship {StarshipId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving starship data");
        }
    }
}