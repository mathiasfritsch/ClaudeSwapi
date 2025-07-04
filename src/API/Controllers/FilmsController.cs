using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly IFilmService _filmService;
    private readonly ILogger<FilmsController> _logger;

    public FilmsController(IFilmService filmService, ILogger<FilmsController> logger)
    {
        _filmService = filmService;
        _logger = logger;
    }

    /// <summary>
    /// Get a paginated list of films from SWAPI
    /// </summary>
    /// <param name="page">Page number (optional)</param>
    /// <param name="limit">Number of items per page (optional)</param>
    /// <returns>Paginated list of films</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponseDto<FilmSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponseDto<FilmSummaryDto>>> GetFilms(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            _logger.LogInformation("Getting films with page: {Page}, limit: {Limit}", page, limit);
            
            var result = await _filmService.GetFilmsAsync(page, limit);
            
            _logger.LogInformation("Successfully retrieved {Count} films", result.Results.Count);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting films");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving films data");
        }
    }

    /// <summary>
    /// Get a specific film by ID from SWAPI
    /// </summary>
    /// <param name="id">Film ID</param>
    /// <returns>Film details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FilmDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FilmDto>> GetFilm(string id)
    {
        try
        {
            _logger.LogInformation("Getting film with ID: {FilmId}", id);
            
            var film = await _filmService.GetFilmAsync(id);
            
            if (film == null)
            {
                _logger.LogWarning("Film with ID {FilmId} not found", id);
                return NotFound($"Film with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved film: {FilmTitle}", film.Title);
            
            return Ok(film);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting film {FilmId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving film data");
        }
    }
}