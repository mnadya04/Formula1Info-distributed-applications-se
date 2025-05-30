using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

public class ApiAuthService : IApiAuthService
{
    private readonly IHttpClientFactory _factory;
    private readonly IHttpContextAccessor _context;

    public ApiAuthService(IHttpClientFactory factory, IHttpContextAccessor context)
    {
        _factory = factory;
        _context = context;
    }

    public async Task<LoginResponseViewModel?> LoginAsync(LoginViewModel model)
    {
        var client = _factory.CreateClient("ApiClient");

        var json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("auth/login", content);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<LoginResponseViewModel>(responseContent);
        if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token)) return null;

        //  Create claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginResponse.Email),
            new Claim(ClaimTypes.NameIdentifier, loginResponse.UserId),
            new Claim("access_token", loginResponse.Token)
        };

        if (loginResponse.IsAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        //  Create identity & sign in
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe,
            ExpiresUtc = loginResponse.Expiration ?? DateTime.UtcNow.AddHours(1)
        };

        await _context.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties
        );

        return loginResponse;
    }

    public async Task LogoutAsync()
    {
        await _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
