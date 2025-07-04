using Application.DTOs;

namespace Application.Interfaces;

public interface IFilmService
{
    Task<PaginatedResponseDto<FilmSummaryDto>> GetFilmsAsync(int? page = null, int? limit = null);
    Task<FilmDto?> GetFilmAsync(string id);
}