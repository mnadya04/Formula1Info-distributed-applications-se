using Formula1InfoMVC.ViewModels.Race;
using Formula1InfoMVC.ViewModels.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Formula1InfoMVC.Services.Interfaces
{
    public interface IRaceApiService
    {
        Task<PagedResult<RaceViewModel>> GetAllAsync(string? name, bool? isFuture, int page = 1, int pageSize = 10);
        Task<RaceFormViewModel?> GetByIdAsync(string id);
        Task<IEnumerable<SelectListItem>> GetDriverOptionsAsync();
        Task<bool> CreateAsync(RaceFormViewModel model);
        Task<bool> UpdateAsync(RaceFormViewModel model);
        Task<bool> DeleteAsync(string id);
    }
}
