using Domain.Models;

namespace Application.Interfaces;

public interface ISwapiClient
{
    // People
    Task<PaginatedResponse<Person>?> GetPeopleAsync(int? page = null, int? limit = null);
    Task<Person?> GetPersonAsync(string id);

    // Films
    Task<PaginatedResponse<Film>?> GetFilmsAsync(int? page = null, int? limit = null);
    Task<Film?> GetFilmAsync(string id);

    // Planets
    Task<PaginatedResponse<Planet>?> GetPlanetsAsync(int? page = null, int? limit = null);
    Task<Planet?> GetPlanetAsync(string id);

    // Species
    Task<PaginatedResponse<Species>?> GetSpeciesAsync(int? page = null, int? limit = null);
    Task<Species?> GetSpeciesDetailAsync(string id);

    // Starships
    Task<PaginatedResponse<Starship>?> GetStarshipsAsync(int? page = null, int? limit = null);
    Task<Starship?> GetStarshipAsync(string id);

    // Vehicles
    Task<PaginatedResponse<Vehicle>?> GetVehiclesAsync(int? page = null, int? limit = null);
    Task<Vehicle?> GetVehicleAsync(string id);
}