using System;
using Formula1Info.DTOs.Helpers;
using Formula1Info.DTOs.TeamDTOs;

namespace Formula1Info.Services.Interfaces
{
    public interface ITeamService
    {
        Task<PagedResult<TeamListDto>> GetAllAsync(string? name = null, string? country = null, int page = 1, int pageSize = 10);
        Task<TeamDetailDto?> GetByIdAsync(string id);
        Task<bool> CreateAsync(TeamCreateDto dto);
        Task<bool> UpdateAsync(TeamUpdateDto dto);
        Task<bool> DeleteAsync(string id);
    }

}

