using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeciesController : ControllerBase
{
    private readonly ISpeciesService _speciesService;
    private readonly ILogger<SpeciesController> _logger;

    public SpeciesController(ISpeciesService speciesService, ILogger<SpeciesController> logger)
    {
        _speciesService = speciesService;
        _logger = logger;
    }

    /// <summary>
    /// Get a paginated list of species from SWAPI
    /// </summary>
    /// <param name="page">Page number (optional)</param>
    /// <param name="limit">Number of items per page (optional)</param>
    /// <returns>Paginated list of species</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponseDto<SpeciesSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponseDto<SpeciesSummaryDto>>> GetSpecies(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            _logger.LogInformation("Getting species with page: {Page}, limit: {Limit}", page, limit);
            
            var result = await _speciesService.GetSpeciesAsync(page, limit);
            
            _logger.LogInformation("Successfully retrieved {Count} species", result.Results.Count);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting species");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving species data");
        }
    }

    /// <summary>
    /// Get a specific species by ID from SWAPI
    /// </summary>
    /// <param name="id">Species ID</param>
    /// <returns>Species details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SpeciesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpeciesDto>> GetSpecies(string id)
    {
        try
        {
            _logger.LogInformation("Getting species with ID: {SpeciesId}", id);
            
            var species = await _speciesService.GetSpeciesAsync(id);
            
            if (species == null)
            {
                _logger.LogWarning("Species with ID {SpeciesId} not found", id);
                return NotFound($"Species with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved species: {SpeciesName}", species.Name);
            
            return Ok(species);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting species {SpeciesId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving species data");
        }
    }
}