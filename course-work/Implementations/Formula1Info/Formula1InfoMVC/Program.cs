using System.Net;
using Formula1InfoMVC.Services;
using Formula1InfoMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// MVC и достъп до контекста
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

//  Cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/Login";
    });

//  Конфигурация на API клиента
var apiBaseAddress = builder.Configuration["ApiSettings:BaseAddress"];
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiBaseAddress);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseCookies = false 
});

builder.Services.AddScoped<ITeamApiService, TeamApiService>();
builder.Services.AddScoped<IDriverApiService, DriverApiService>();
builder.Services.AddScoped<IRaceApiService, RaceApiService>();
builder.Services.AddScoped<IApiAuthService, ApiAuthService>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //  Преди authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
