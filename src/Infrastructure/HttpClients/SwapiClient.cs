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
}