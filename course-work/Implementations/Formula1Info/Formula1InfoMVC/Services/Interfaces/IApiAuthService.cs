using Formula1InfoMVC.ViewModels.Account;

namespace Formula1InfoMVC.Services.Interfaces
{
    public interface IApiAuthService
    {
        Task<LoginResponseViewModel?> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
