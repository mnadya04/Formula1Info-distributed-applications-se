using Formula1InfoMVC.ViewModels.Helpers;
using Formula1InfoMVC.ViewModels.Team;

namespace Formula1InfoMVC.Services.Interfaces
{
    public interface ITeamApiService
    {
        Task<PagedResult<TeamViewModel>> GetAllAsync(string? name, string? country, int page = 1, int pageSize = 5);
        Task<TeamFormViewModel?> GetByIdAsync(string id);
        Task<bool> CreateAsync(TeamFormViewModel model);
        Task<bool> UpdateAsync(TeamFormViewModel model);
        Task<bool> DeleteAsync(string id);
    }
}
