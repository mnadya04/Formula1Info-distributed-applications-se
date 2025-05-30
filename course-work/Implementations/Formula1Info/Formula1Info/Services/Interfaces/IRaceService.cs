using System;
using Formula1Info.DTOs.RaceDTOs;
using Formula1Info.DTOs.Helpers;

namespace Formula1Info.Services.Interfaces
{
    public interface IRaceService
    {
        Task<PagedResult<RaceListDto>> GetAllAsync(string? name = null, bool? isFuture = null, int page = 1, int pageSize = 10);
        Task<RaceDetailDto?> GetByIdAsync(string id);
        Task<bool> CreateAsync(RaceCreateDto dto);
        Task<bool> UpdateAsync(RaceUpdateDto dto);
        Task<bool> DeleteAsync(string id);
    }


}

