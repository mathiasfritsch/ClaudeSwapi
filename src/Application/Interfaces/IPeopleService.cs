using Application.DTOs;

namespace Application.Interfaces;

public interface IPeopleService
{
    Task<PaginatedResponseDto<PersonSummaryDto>> GetPeopleAsync(int? page = null, int? limit = null);
    Task<PersonDto?> GetPersonAsync(string id);
}