using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PeopleService : IPeopleService
{
    private readonly ISwapiClient _swapiClient;
    private readonly ILogger<PeopleService> _logger;

    public PeopleService(ISwapiClient swapiClient, ILogger<PeopleService> logger)
    {
        _swapiClient = swapiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponseDto<PersonSummaryDto>> GetPeopleAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _swapiClient.GetPeopleAsync(page, limit);
            
            if (response == null)
            {
                _logger.LogWarning("SWAPI returned null response for people request");
                return new PaginatedResponseDto<PersonSummaryDto>();
            }

            return new PaginatedResponseDto<PersonSummaryDto>
            {
                Message = response.Message,
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Previous = response.Previous,
                Next = response.Next,
                Results = response.Results.Select(MapToPersonSummaryDto).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching people from SWAPI");
            throw;
        }
    }

    public async Task<PersonDto?> GetPersonAsync(string id)
    {
        try
        {
            var person = await _swapiClient.GetPersonAsync(id);
            
            if (person == null)
            {
                _logger.LogWarning("SWAPI returned null for person with ID: {PersonId}", id);
                return null;
            }

            return MapToPersonDto(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching person {PersonId} from SWAPI", id);
            throw;
        }
    }

    private static PersonSummaryDto MapToPersonSummaryDto(Person person)
    {
        return new PersonSummaryDto
        {
            Uid = person.Uid,
            Name = person.Name,
            Url = person.Url
        };
    }

    private static PersonDto MapToPersonDto(Person person)
    {
        return new PersonDto
        {
            Uid = person.Uid,
            Name = person.Name,
            Gender = person.Gender,
            SkinColor = person.SkinColor,
            HairColor = person.HairColor,
            Height = person.Height,
            EyeColor = person.EyeColor,
            Mass = person.Mass,
            BirthYear = person.BirthYear,
            Homeworld = person.Homeworld,
            Created = person.Created,
            Edited = person.Edited,
            Url = person.Url
        };
    }
}