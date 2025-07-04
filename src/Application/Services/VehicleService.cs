using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class VehicleService : IVehicleService
{
    private readonly ISwapiClient _swapiClient;
    private readonly ILogger<VehicleService> _logger;

    public VehicleService(ISwapiClient swapiClient, ILogger<VehicleService> logger)
    {
        _swapiClient = swapiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponseDto<VehicleSummaryDto>> GetVehiclesAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _swapiClient.GetVehiclesAsync(page, limit);
            
            if (response == null)
            {
                _logger.LogWarning("SWAPI returned null response for vehicles request");
                return new PaginatedResponseDto<VehicleSummaryDto>();
            }

            return new PaginatedResponseDto<VehicleSummaryDto>
            {
                Message = response.Message,
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Previous = response.Previous,
                Next = response.Next,
                Results = response.Results.Select(MapToVehicleSummaryDto).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching vehicles from SWAPI");
            throw;
        }
    }

    public async Task<VehicleDto?> GetVehicleAsync(string id)
    {
        try
        {
            var vehicle = await _swapiClient.GetVehicleAsync(id);
            
            if (vehicle == null)
            {
                _logger.LogWarning("SWAPI returned null for vehicle with ID: {VehicleId}", id);
                return null;
            }

            return MapToVehicleDto(vehicle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching vehicle {VehicleId} from SWAPI", id);
            throw;
        }
    }

    private static VehicleSummaryDto MapToVehicleSummaryDto(Vehicle vehicle)
    {
        return new VehicleSummaryDto
        {
            Uid = vehicle.Uid,
            Name = vehicle.Name,
            Url = vehicle.Url
        };
    }

    private static VehicleDto MapToVehicleDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Uid = vehicle.Uid,
            Name = vehicle.Name,
            Model = vehicle.Model,
            Manufacturer = vehicle.Manufacturer,
            CostInCredits = vehicle.CostInCredits,
            Length = vehicle.Length,
            MaxAtmospheringSpeed = vehicle.MaxAtmospheringSpeed,
            Crew = vehicle.Crew,
            Passengers = vehicle.Passengers,
            CargoCapacity = vehicle.CargoCapacity,
            Consumables = vehicle.Consumables,
            VehicleClass = vehicle.VehicleClass,
            Pilots = vehicle.Pilots,
            Films = vehicle.Films,
            Created = vehicle.Created,
            Edited = vehicle.Edited,
            Url = vehicle.Url
        };
    }
}