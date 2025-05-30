using System;
using Formula1InfoMVC.ViewModels.Driver;
using Formula1InfoMVC.ViewModels.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Formula1InfoMVC.Services.Interfaces
{
    public interface IDriverApiService
    {
        Task<List<DriverViewModel>> GetAllAsync();
        Task<DriverFormViewModel?> GetByIdAsync(string id);
        Task<PagedResult<DriverViewModel>> GetFilteredAsync(string? nationality, string? fullName, string? sort, int page, int pageSize);

        Task<IEnumerable<SelectListItem>> GetTeamOptionsAsync();
        Task<bool> CreateAsync(DriverFormViewModel model);
        Task<bool> UpdateAsync(DriverFormViewModel model);
        Task<bool> DeleteAsync(string id);
    }

}

