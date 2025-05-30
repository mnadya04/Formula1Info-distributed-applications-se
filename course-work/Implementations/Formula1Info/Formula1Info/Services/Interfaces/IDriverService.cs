using System;
using Formula1Info.DTOs.DriverDTOs;
using Formula1Info.DTOs.Helpers;

namespace Formula1Info.Services.Interfaces
{

    public interface IDriverService
    {
        Task<PagedResult<DriverListDto>> GetAllAsync(string? nationality = null, string? fullName = null, string? sort = null, int page = 1, int pageSize = 10);
        Task<DriverDetailDto?> GetByIdAsync(string id);
        Task<bool> CreateAsync(DriverCreateDto dto);
        Task<bool> UpdateAsync(DriverUpdateDto dto);
        Task<bool> DeleteAsync(string id);
    }
}

