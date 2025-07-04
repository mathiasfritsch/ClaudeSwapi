using Refit;

namespace Infrastructure.HttpClients;

public interface ISwapiApiClient
{
    [Get("/people")]
    Task<ApiResponse<SwapiPaginatedResponse<SwapiPersonSummary>>> GetPeopleAsync(
        [Query] int? page = null, 
        [Query] int? limit = null);

    [Get("/people/{id}")]
    Task<ApiResponse<SwapiPersonDetailResponse>> GetPersonAsync(string id);

    // Films
    [Get("/films")]
    Task<ApiResponse<SwapiPaginatedResponse<SwapiFilmSummary>>> GetFilmsAsync(
        [Query] int? page = null, 
        [Query] int? limit = null);

    [Get("/films/{id}")]
    Task<ApiResponse<SwapiFilmDetailResponse>> GetFilmAsync(string id);

    // Planets
    [Get("/planets")]
    Task<ApiResponse<SwapiPaginatedResponse<SwapiPlanetSummary>>> GetPlanetsAsync(
        [Query] int? page = null, 
        [Query] int? limit = null);

    [Get("/planets/{id}")]
    Task<ApiResponse<SwapiPlanetDetailResponse>> GetPlanetAsync(string id);

    // Species
    [Get("/species")]
    Task<ApiResponse<SwapiPaginatedResponse<SwapiSpeciesSummary>>> GetSpeciesAsync(
        [Query] int? page = null, 
        [Query] int? limit = null);

    [Get("/species/{id}")]
    Task<ApiResponse<SwapiSpeciesDetailResponse>> GetSpeciesDetailAsync(string id);

    // Starships
    [Get("/starships")]
    Task<ApiResponse<SwapiPaginatedResponse<SwapiStarshipSummary>>> GetStarshipsAsync(
        [Query] int? page = null, 
        [Query] int? limit = null);

    [Get("/starships/{id}")]
    Task<ApiResponse<SwapiStarshipDetailResponse>> GetStarshipAsync(string id);

    // Vehicles
    [Get("/vehicles")]
    Task<ApiResponse<SwapiPaginatedResponse<SwapiVehicleSummary>>> GetVehiclesAsync(
        [Query] int? page = null, 
        [Query] int? limit = null);

    [Get("/vehicles/{id}")]
    Task<ApiResponse<SwapiVehicleDetailResponse>> GetVehicleAsync(string id);
}