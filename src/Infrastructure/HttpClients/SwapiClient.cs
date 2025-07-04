using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.HttpClients;

public class SwapiClient : ISwapiClient
{
    private readonly ISwapiApiClient _apiClient;
    private readonly ILogger<SwapiClient> _logger;

    public SwapiClient(ISwapiApiClient apiClient, ILogger<SwapiClient> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponse<Person>?> GetPeopleAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _apiClient.GetPeopleAsync(page, limit);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return new PaginatedResponse<Person>
            {
                Message = swapiResponse.Message,
                TotalRecords = swapiResponse.TotalRecords,
                TotalPages = swapiResponse.TotalPages,
                Previous = swapiResponse.Previous,
                Next = swapiResponse.Next,
                Results = swapiResponse.Results.Select(MapToPersonSummary).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI people endpoint");
            throw;
        }
    }

    public async Task<Person?> GetPersonAsync(string id)
    {
        try
        {
            var response = await _apiClient.GetPersonAsync(id);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed for person {PersonId} with status code: {StatusCode}", id, response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return MapToPerson(swapiResponse.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI person endpoint for ID: {PersonId}", id);
            throw;
        }
    }

    private static Person MapToPersonSummary(SwapiPersonSummary summary)
    {
        return new Person
        {
            Uid = summary.Uid,
            Name = summary.Name,
            Url = summary.Url,
            Created = DateTime.MinValue,
            Edited = DateTime.MinValue
        };
    }

    private static Person MapToPerson(SwapiPersonDetail detail)
    {
        return new Person
        {
            Uid = detail.Uid,
            Name = detail.Properties.Name,
            Gender = detail.Properties.Gender,
            SkinColor = detail.Properties.SkinColor,
            HairColor = detail.Properties.HairColor,
            Height = detail.Properties.Height,
            EyeColor = detail.Properties.EyeColor,
            Mass = detail.Properties.Mass,
            BirthYear = detail.Properties.BirthYear,
            Homeworld = detail.Properties.Homeworld,
            Created = detail.Properties.Created,
            Edited = detail.Properties.Edited,
            Url = detail.Properties.Url
        };
    }

    // Films
    public async Task<PaginatedResponse<Film>?> GetFilmsAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _apiClient.GetFilmsAsync(page, limit);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return new PaginatedResponse<Film>
            {
                Message = swapiResponse.Message,
                TotalRecords = swapiResponse.TotalRecords,
                TotalPages = swapiResponse.TotalPages,
                Previous = swapiResponse.Previous,
                Next = swapiResponse.Next,
                Results = swapiResponse.Results.Select(MapToFilmSummary).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI films endpoint");
            throw;
        }
    }

    public async Task<Film?> GetFilmAsync(string id)
    {
        try
        {
            var response = await _apiClient.GetFilmAsync(id);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed for film {FilmId} with status code: {StatusCode}", id, response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return MapToFilm(swapiResponse.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI film endpoint for ID: {FilmId}", id);
            throw;
        }
    }

    // Planets
    public async Task<PaginatedResponse<Planet>?> GetPlanetsAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _apiClient.GetPlanetsAsync(page, limit);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return new PaginatedResponse<Planet>
            {
                Message = swapiResponse.Message,
                TotalRecords = swapiResponse.TotalRecords,
                TotalPages = swapiResponse.TotalPages,
                Previous = swapiResponse.Previous,
                Next = swapiResponse.Next,
                Results = swapiResponse.Results.Select(MapToPlanetSummary).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI planets endpoint");
            throw;
        }
    }

    public async Task<Planet?> GetPlanetAsync(string id)
    {
        try
        {
            var response = await _apiClient.GetPlanetAsync(id);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed for planet {PlanetId} with status code: {StatusCode}", id, response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return MapToPlanet(swapiResponse.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI planet endpoint for ID: {PlanetId}", id);
            throw;
        }
    }

    // Species
    public async Task<PaginatedResponse<Species>?> GetSpeciesAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _apiClient.GetSpeciesAsync(page, limit);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return new PaginatedResponse<Species>
            {
                Message = swapiResponse.Message,
                TotalRecords = swapiResponse.TotalRecords,
                TotalPages = swapiResponse.TotalPages,
                Previous = swapiResponse.Previous,
                Next = swapiResponse.Next,
                Results = swapiResponse.Results.Select(MapToSpeciesSummary).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI species endpoint");
            throw;
        }
    }

    public async Task<Species?> GetSpeciesDetailAsync(string id)
    {
        try
        {
            var response = await _apiClient.GetSpeciesDetailAsync(id);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed for species {SpeciesId} with status code: {StatusCode}", id, response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return MapToSpecies(swapiResponse.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI species endpoint for ID: {SpeciesId}", id);
            throw;
        }
    }

    // Starships
    public async Task<PaginatedResponse<Starship>?> GetStarshipsAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _apiClient.GetStarshipsAsync(page, limit);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return new PaginatedResponse<Starship>
            {
                Message = swapiResponse.Message,
                TotalRecords = swapiResponse.TotalRecords,
                TotalPages = swapiResponse.TotalPages,
                Previous = swapiResponse.Previous,
                Next = swapiResponse.Next,
                Results = swapiResponse.Results.Select(MapToStarshipSummary).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI starships endpoint");
            throw;
        }
    }

    public async Task<Starship?> GetStarshipAsync(string id)
    {
        try
        {
            var response = await _apiClient.GetStarshipAsync(id);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed for starship {StarshipId} with status code: {StatusCode}", id, response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return MapToStarship(swapiResponse.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI starship endpoint for ID: {StarshipId}", id);
            throw;
        }
    }

    // Vehicles
    public async Task<PaginatedResponse<Vehicle>?> GetVehiclesAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _apiClient.GetVehiclesAsync(page, limit);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return new PaginatedResponse<Vehicle>
            {
                Message = swapiResponse.Message,
                TotalRecords = swapiResponse.TotalRecords,
                TotalPages = swapiResponse.TotalPages,
                Previous = swapiResponse.Previous,
                Next = swapiResponse.Next,
                Results = swapiResponse.Results.Select(MapToVehicleSummary).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI vehicles endpoint");
            throw;
        }
    }

    public async Task<Vehicle?> GetVehicleAsync(string id)
    {
        try
        {
            var response = await _apiClient.GetVehicleAsync(id);
            
            if (!response.IsSuccessStatusCode || response.Content == null)
            {
                _logger.LogWarning("SWAPI API call failed for vehicle {VehicleId} with status code: {StatusCode}", id, response.StatusCode);
                return null;
            }

            var swapiResponse = response.Content;
            return MapToVehicle(swapiResponse.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling SWAPI vehicle endpoint for ID: {VehicleId}", id);
            throw;
        }
    }

    // Mapping methods for Films
    private static Film MapToFilmSummary(SwapiFilmSummary summary)
    {
        return new Film
        {
            Uid = summary.Uid,
            Title = summary.Properties.Title,
            EpisodeId = summary.Properties.EpisodeId,
            Director = summary.Properties.Director,
            Producer = summary.Properties.Producer,
            ReleaseDate = DateTime.TryParse(summary.Properties.ReleaseDate, out var releaseDate) ? releaseDate : DateTime.MinValue,
            Url = summary.Url,
            Created = DateTime.MinValue,
            Edited = DateTime.MinValue
        };
    }

    private static Film MapToFilm(SwapiFilmDetail detail)
    {
        return new Film
        {
            Uid = detail.Uid,
            Title = detail.Properties.Title,
            EpisodeId = detail.Properties.EpisodeId,
            OpeningCrawl = detail.Properties.OpeningCrawl,
            Director = detail.Properties.Director,
            Producer = detail.Properties.Producer,
            ReleaseDate = DateTime.TryParse(detail.Properties.ReleaseDate, out var releaseDate) ? releaseDate : DateTime.MinValue,
            Characters = detail.Properties.Characters,
            Planets = detail.Properties.Planets,
            Starships = detail.Properties.Starships,
            Vehicles = detail.Properties.Vehicles,
            Species = detail.Properties.Species,
            Created = detail.Properties.Created,
            Edited = detail.Properties.Edited,
            Url = detail.Properties.Url
        };
    }

    // Mapping methods for Planets
    private static Planet MapToPlanetSummary(SwapiPlanetSummary summary)
    {
        return new Planet
        {
            Uid = summary.Uid,
            Name = summary.Name,
            Url = summary.Url,
            Created = DateTime.MinValue,
            Edited = DateTime.MinValue
        };
    }

    private static Planet MapToPlanet(SwapiPlanetDetail detail)
    {
        return new Planet
        {
            Uid = detail.Uid,
            Name = detail.Properties.Name,
            RotationPeriod = detail.Properties.RotationPeriod,
            OrbitalPeriod = detail.Properties.OrbitalPeriod,
            Diameter = detail.Properties.Diameter,
            Climate = detail.Properties.Climate,
            Gravity = detail.Properties.Gravity,
            Terrain = detail.Properties.Terrain,
            SurfaceWater = detail.Properties.SurfaceWater,
            Population = detail.Properties.Population,
            Residents = detail.Properties.Residents,
            Films = detail.Properties.Films,
            Created = detail.Properties.Created,
            Edited = detail.Properties.Edited,
            Url = detail.Properties.Url
        };
    }

    // Mapping methods for Species
    private static Species MapToSpeciesSummary(SwapiSpeciesSummary summary)
    {
        return new Species
        {
            Uid = summary.Uid,
            Name = summary.Name,
            Url = summary.Url,
            Created = DateTime.MinValue,
            Edited = DateTime.MinValue
        };
    }

    private static Species MapToSpecies(SwapiSpeciesDetail detail)
    {
        return new Species
        {
            Uid = detail.Uid,
            Name = detail.Properties.Name,
            Classification = detail.Properties.Classification,
            Designation = detail.Properties.Designation,
            AverageHeight = detail.Properties.AverageHeight,
            SkinColors = detail.Properties.SkinColors,
            HairColors = detail.Properties.HairColors,
            EyeColors = detail.Properties.EyeColors,
            AverageLifespan = detail.Properties.AverageLifespan,
            Homeworld = detail.Properties.Homeworld,
            Language = detail.Properties.Language,
            People = detail.Properties.People,
            Films = detail.Properties.Films,
            Created = detail.Properties.Created,
            Edited = detail.Properties.Edited,
            Url = detail.Properties.Url
        };
    }

    // Mapping methods for Starships
    private static Starship MapToStarshipSummary(SwapiStarshipSummary summary)
    {
        return new Starship
        {
            Uid = summary.Uid,
            Name = summary.Name,
            Url = summary.Url,
            Created = DateTime.MinValue,
            Edited = DateTime.MinValue
        };
    }

    private static Starship MapToStarship(SwapiStarshipDetail detail)
    {
        return new Starship
        {
            Uid = detail.Uid,
            Name = detail.Properties.Name,
            Model = detail.Properties.Model,
            Manufacturer = detail.Properties.Manufacturer,
            CostInCredits = detail.Properties.CostInCredits,
            Length = detail.Properties.Length,
            MaxAtmospheringSpeed = detail.Properties.MaxAtmospheringSpeed,
            Crew = detail.Properties.Crew,
            Passengers = detail.Properties.Passengers,
            CargoCapacity = detail.Properties.CargoCapacity,
            Consumables = detail.Properties.Consumables,
            HyperdriveRating = detail.Properties.HyperdriveRating,
            MGLT = detail.Properties.MGLT,
            StarshipClass = detail.Properties.StarshipClass,
            Pilots = detail.Properties.Pilots,
            Films = detail.Properties.Films,
            Created = detail.Properties.Created,
            Edited = detail.Properties.Edited,
            Url = detail.Properties.Url
        };
    }

    // Mapping methods for Vehicles
    private static Vehicle MapToVehicleSummary(SwapiVehicleSummary summary)
    {
        return new Vehicle
        {
            Uid = summary.Uid,
            Name = summary.Name,
            Url = summary.Url,
            Created = DateTime.MinValue,
            Edited = DateTime.MinValue
        };
    }

    private static Vehicle MapToVehicle(SwapiVehicleDetail detail)
    {
        return new Vehicle
        {
            Uid = detail.Uid,
            Name = detail.Properties.Name,
            Model = detail.Properties.Model,
            Manufacturer = detail.Properties.Manufacturer,
            CostInCredits = detail.Properties.CostInCredits,
            Length = detail.Properties.Length,
            MaxAtmospheringSpeed = detail.Properties.MaxAtmospheringSpeed,
            Crew = detail.Properties.Crew,
            Passengers = detail.Properties.Passengers,
            CargoCapacity = detail.Properties.CargoCapacity,
            Consumables = detail.Properties.Consumables,
            VehicleClass = detail.Properties.VehicleClass,
            Pilots = detail.Properties.Pilots,
            Films = detail.Properties.Films,
            Created = detail.Properties.Created,
            Edited = detail.Properties.Edited,
            Url = detail.Properties.Url
        };
    }
}