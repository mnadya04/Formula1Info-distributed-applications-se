using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Formula1InfoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiAuthService _authService;

        public AccountController(IApiAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.LoginAsync(model);
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
