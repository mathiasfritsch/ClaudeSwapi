using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPeopleService _peopleService;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IPeopleService peopleService, ILogger<PeopleController> logger)
    {
        _peopleService = peopleService;
        _logger = logger;
    }

    /// <summary>
    /// Get a paginated list of people from SWAPI
    /// </summary>
    /// <param name="page">Page number (optional)</param>
    /// <param name="limit">Number of items per page (optional)</param>
    /// <returns>Paginated list of people</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponseDto<PersonSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponseDto<PersonSummaryDto>>> GetPeople(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            _logger.LogInformation("Getting people with page: {Page}, limit: {Limit}", page, limit);
            
            var result = await _peopleService.GetPeopleAsync(page, limit);
            
            _logger.LogInformation("Successfully retrieved {Count} people", result.Results.Count);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting people");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving people data");
        }
    }

    /// <summary>
    /// Get a specific person by ID from SWAPI
    /// </summary>
    /// <param name="id">Person ID</param>
    /// <returns>Person details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PersonDto>> GetPerson(string id)
    {
        try
        {
            _logger.LogInformation("Getting person with ID: {PersonId}", id);
            
            var person = await _peopleService.GetPersonAsync(id);
            
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", id);
                return NotFound($"Person with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved person: {PersonName}", person.Name);
            
            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting person {PersonId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving person data");
        }
    }
}