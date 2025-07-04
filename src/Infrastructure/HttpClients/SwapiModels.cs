using System.Text.Json.Serialization;

namespace Infrastructure.HttpClients;

public class SwapiPaginatedResponse<T>
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("total_records")]
    public int TotalRecords { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("previous")]
    public string? Previous { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("results")]
    public List<T> Results { get; set; } = new();
}

public class SwapiPersonSummary
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiPersonDetailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public SwapiPersonDetail Result { get; set; } = new();
}

public class SwapiPersonDetail
{
    [JsonPropertyName("properties")]
    public SwapiPersonProperties Properties { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int Version { get; set; }
}

public class SwapiPersonProperties
{
    [JsonPropertyName("height")]
    public string Height { get; set; } = string.Empty;

    [JsonPropertyName("mass")]
    public string Mass { get; set; } = string.Empty;

    [JsonPropertyName("hair_color")]
    public string HairColor { get; set; } = string.Empty;

    [JsonPropertyName("skin_color")]
    public string SkinColor { get; set; } = string.Empty;

    [JsonPropertyName("eye_color")]
    public string EyeColor { get; set; } = string.Empty;

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("homeworld")]
    public string Homeworld { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

// Films
public class SwapiFilmSummary
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public SwapiFilmSummaryProperties Properties { get; set; } = new();

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiFilmSummaryProperties
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("episode_id")]
    public int EpisodeId { get; set; }

    [JsonPropertyName("director")]
    public string Director { get; set; } = string.Empty;

    [JsonPropertyName("producer")]
    public string Producer { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;
}

public class SwapiFilmDetailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public SwapiFilmDetail Result { get; set; } = new();
}

public class SwapiFilmDetail
{
    [JsonPropertyName("properties")]
    public SwapiFilmProperties Properties { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int Version { get; set; }
}

public class SwapiFilmProperties
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("episode_id")]
    public int EpisodeId { get; set; }

    [JsonPropertyName("opening_crawl")]
    public string OpeningCrawl { get; set; } = string.Empty;

    [JsonPropertyName("director")]
    public string Director { get; set; } = string.Empty;

    [JsonPropertyName("producer")]
    public string Producer { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;

    [JsonPropertyName("characters")]
    public List<string> Characters { get; set; } = new();

    [JsonPropertyName("planets")]
    public List<string> Planets { get; set; } = new();

    [JsonPropertyName("starships")]
    public List<string> Starships { get; set; } = new();

    [JsonPropertyName("vehicles")]
    public List<string> Vehicles { get; set; } = new();

    [JsonPropertyName("species")]
    public List<string> Species { get; set; } = new();

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

// Planets
public class SwapiPlanetSummary
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiPlanetDetailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public SwapiPlanetDetail Result { get; set; } = new();
}

public class SwapiPlanetDetail
{
    [JsonPropertyName("properties")]
    public SwapiPlanetProperties Properties { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int Version { get; set; }
}

public class SwapiPlanetProperties
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("rotation_period")]
    public string RotationPeriod { get; set; } = string.Empty;

    [JsonPropertyName("orbital_period")]
    public string OrbitalPeriod { get; set; } = string.Empty;

    [JsonPropertyName("diameter")]
    public string Diameter { get; set; } = string.Empty;

    [JsonPropertyName("climate")]
    public string Climate { get; set; } = string.Empty;

    [JsonPropertyName("gravity")]
    public string Gravity { get; set; } = string.Empty;

    [JsonPropertyName("terrain")]
    public string Terrain { get; set; } = string.Empty;

    [JsonPropertyName("surface_water")]
    public string SurfaceWater { get; set; } = string.Empty;

    [JsonPropertyName("population")]
    public string Population { get; set; } = string.Empty;

    [JsonPropertyName("residents")]
    public List<string> Residents { get; set; } = new();

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new();

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

// Species
public class SwapiSpeciesSummary
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiSpeciesDetailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public SwapiSpeciesDetail Result { get; set; } = new();
}

public class SwapiSpeciesDetail
{
    [JsonPropertyName("properties")]
    public SwapiSpeciesProperties Properties { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int Version { get; set; }
}

public class SwapiSpeciesProperties
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("classification")]
    public string Classification { get; set; } = string.Empty;

    [JsonPropertyName("designation")]
    public string Designation { get; set; } = string.Empty;

    [JsonPropertyName("average_height")]
    public string AverageHeight { get; set; } = string.Empty;

    [JsonPropertyName("skin_colors")]
    public string SkinColors { get; set; } = string.Empty;

    [JsonPropertyName("hair_colors")]
    public string HairColors { get; set; } = string.Empty;

    [JsonPropertyName("eye_colors")]
    public string EyeColors { get; set; } = string.Empty;

    [JsonPropertyName("average_lifespan")]
    public string AverageLifespan { get; set; } = string.Empty;

    [JsonPropertyName("homeworld")]
    public string Homeworld { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("people")]
    public List<string> People { get; set; } = new();

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new();

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

// Starships
public class SwapiStarshipSummary
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiStarshipDetailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public SwapiStarshipDetail Result { get; set; } = new();
}

public class SwapiStarshipDetail
{
    [JsonPropertyName("properties")]
    public SwapiStarshipProperties Properties { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int Version { get; set; }
}

public class SwapiStarshipProperties
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; set; } = string.Empty;

    [JsonPropertyName("cost_in_credits")]
    public string CostInCredits { get; set; } = string.Empty;

    [JsonPropertyName("length")]
    public string Length { get; set; } = string.Empty;

    [JsonPropertyName("max_atmosphering_speed")]
    public string MaxAtmospheringSpeed { get; set; } = string.Empty;

    [JsonPropertyName("crew")]
    public string Crew { get; set; } = string.Empty;

    [JsonPropertyName("passengers")]
    public string Passengers { get; set; } = string.Empty;

    [JsonPropertyName("cargo_capacity")]
    public string CargoCapacity { get; set; } = string.Empty;

    [JsonPropertyName("consumables")]
    public string Consumables { get; set; } = string.Empty;

    [JsonPropertyName("hyperdrive_rating")]
    public string HyperdriveRating { get; set; } = string.Empty;

    [JsonPropertyName("MGLT")]
    public string MGLT { get; set; } = string.Empty;

    [JsonPropertyName("starship_class")]
    public string StarshipClass { get; set; } = string.Empty;

    [JsonPropertyName("pilots")]
    public List<string> Pilots { get; set; } = new();

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new();

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

// Vehicles
public class SwapiVehicleSummary
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiVehicleDetailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public SwapiVehicleDetail Result { get; set; } = new();
}

public class SwapiVehicleDetail
{
    [JsonPropertyName("properties")]
    public SwapiVehicleProperties Properties { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int Version { get; set; }
}

public class SwapiVehicleProperties
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; set; } = string.Empty;

    [JsonPropertyName("cost_in_credits")]
    public string CostInCredits { get; set; } = string.Empty;

    [JsonPropertyName("length")]
    public string Length { get; set; } = string.Empty;

    [JsonPropertyName("max_atmosphering_speed")]
    public string MaxAtmospheringSpeed { get; set; } = string.Empty;

    [JsonPropertyName("crew")]
    public string Crew { get; set; } = string.Empty;

    [JsonPropertyName("passengers")]
    public string Passengers { get; set; } = string.Empty;

    [JsonPropertyName("cargo_capacity")]
    public string CargoCapacity { get; set; } = string.Empty;

    [JsonPropertyName("consumables")]
    public string Consumables { get; set; } = string.Empty;

    [JsonPropertyName("vehicle_class")]
    public string VehicleClass { get; set; } = string.Empty;

    [JsonPropertyName("pilots")]
    public List<string> Pilots { get; set; } = new();

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new();

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}