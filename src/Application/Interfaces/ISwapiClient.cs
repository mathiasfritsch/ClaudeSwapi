using Domain.Models;

namespace Application.Interfaces;

public interface ISwapiClient
{
    Task<PaginatedResponse<Person>?> GetPeopleAsync(int? page = null, int? limit = null);
    Task<Person?> GetPersonAsync(string id);
}