using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(IVehicleService vehicleService, ILogger<VehiclesController> logger)
    {
        _vehicleService = vehicleService;
        _logger = logger;
    }

    /// <summary>
    /// Get a paginated list of vehicles from SWAPI
    /// </summary>
    /// <param name="page">Page number (optional)</param>
    /// <param name="limit">Number of items per page (optional)</param>
    /// <returns>Paginated list of vehicles</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponseDto<VehicleSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponseDto<VehicleSummaryDto>>> GetVehicles(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            _logger.LogInformation("Getting vehicles with page: {Page}, limit: {Limit}", page, limit);
            
            var result = await _vehicleService.GetVehiclesAsync(page, limit);
            
            _logger.LogInformation("Successfully retrieved {Count} vehicles", result.Results.Count);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting vehicles");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving vehicles data");
        }
    }

    /// <summary>
    /// Get a specific vehicle by ID from SWAPI
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <returns>Vehicle details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleDto>> GetVehicle(string id)
    {
        try
        {
            _logger.LogInformation("Getting vehicle with ID: {VehicleId}", id);
            
            var vehicle = await _vehicleService.GetVehicleAsync(id);
            
            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle with ID {VehicleId} not found", id);
                return NotFound($"Vehicle with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved vehicle: {VehicleName}", vehicle.Name);
            
            return Ok(vehicle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting vehicle {VehicleId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving vehicle data");
        }
    }
}