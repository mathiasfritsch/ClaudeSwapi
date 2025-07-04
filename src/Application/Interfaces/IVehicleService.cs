using Application.DTOs;

namespace Application.Interfaces;

public interface IVehicleService
{
    Task<PaginatedResponseDto<VehicleSummaryDto>> GetVehiclesAsync(int? page = null, int? limit = null);
    Task<VehicleDto?> GetVehicleAsync(string id);
}