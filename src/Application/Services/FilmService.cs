using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class FilmService : IFilmService
{
    private readonly ISwapiClient _swapiClient;
    private readonly ILogger<FilmService> _logger;

    public FilmService(ISwapiClient swapiClient, ILogger<FilmService> logger)
    {
        _swapiClient = swapiClient;
        _logger = logger;
    }

    public async Task<PaginatedResponseDto<FilmSummaryDto>> GetFilmsAsync(int? page = null, int? limit = null)
    {
        try
        {
            var response = await _swapiClient.GetFilmsAsync(page, limit);
            
            if (response == null)
            {
                _logger.LogWarning("SWAPI returned null response for films request");
                return new PaginatedResponseDto<FilmSummaryDto>();
            }

            return new PaginatedResponseDto<FilmSummaryDto>
            {
                Message = response.Message,
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Previous = response.Previous,
                Next = response.Next,
                Results = response.Results.Select(MapToFilmSummaryDto).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching films from SWAPI");
            throw;
        }
    }

    public async Task<FilmDto?> GetFilmAsync(string id)
    {
        try
        {
            var film = await _swapiClient.GetFilmAsync(id);
            
            if (film == null)
            {
                _logger.LogWarning("SWAPI returned null for film with ID: {FilmId}", id);
                return null;
            }

            return MapToFilmDto(film);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching film {FilmId} from SWAPI", id);
            throw;
        }
    }

    private static FilmSummaryDto MapToFilmSummaryDto(Film film)
    {
        return new FilmSummaryDto
        {
            Uid = film.Uid,
            Title = film.Title,
            EpisodeId = film.EpisodeId,
            Director = film.Director,
            Producer = film.Producer,
            ReleaseDate = film.ReleaseDate,
            Url = film.Url
        };
    }

    private static FilmDto MapToFilmDto(Film film)
    {
        return new FilmDto
        {
            Uid = film.Uid,
            Title = film.Title,
            EpisodeId = film.EpisodeId,
            OpeningCrawl = film.OpeningCrawl,
            Director = film.Director,
            Producer = film.Producer,
            ReleaseDate = film.ReleaseDate,
            Characters = film.Characters,
            Planets = film.Planets,
            Starships = film.Starships,
            Vehicles = film.Vehicles,
            Species = film.Species,
            Created = film.Created,
            Edited = film.Edited,
            Url = film.Url
        };
    }
}