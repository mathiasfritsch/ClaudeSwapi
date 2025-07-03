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
}