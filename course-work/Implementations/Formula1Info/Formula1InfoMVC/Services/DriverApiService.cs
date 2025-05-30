using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Driver;
using Formula1InfoMVC.ViewModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Formula1InfoMVC.Services
{
    public class DriverApiService : IDriverApiService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _httpContext;

        public DriverApiService(IHttpClientFactory factory, IHttpContextAccessor httpContext)
        {
            _factory = factory;
            _httpContext = httpContext;
        }

        private HttpClient CreateClientWithToken()
        {
            var client = _factory.CreateClient("ApiClient");

            var token = _httpContext.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "access_token")?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<List<DriverViewModel>> GetAllAsync()
        {
            var client = CreateClientWithToken();
            var response = await client.GetAsync("drivers");

            if (!response.IsSuccessStatusCode) return new();

            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonConvert.DeserializeObject<PagedResult<DriverViewModel>>(json);
            return wrapper?.Items ?? new List<DriverViewModel>();
        }

        public async Task<DriverFormViewModel?> GetByIdAsync(string id)
        {
            var client = CreateClientWithToken();
            var response = await client.GetAsync($"drivers/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DriverFormViewModel>(json);
        }

        public async Task<PagedResult<DriverViewModel>> GetFilteredAsync(string? nationality, string? fullName, string? sort, int page, int pageSize = 5)
        {
            var client = CreateClientWithToken();
            var query = $"drivers?nationality={nationality}&fullName={fullName}&sort={sort}&page={page}&pageSize={pageSize}";
            var response = await client.GetAsync(query);

            if (!response.IsSuccessStatusCode)
                return new PagedResult<DriverViewModel> { Items = new List<DriverViewModel>(), TotalCount = 0, TotalPages = 0 };

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PagedResult<DriverViewModel>>(json);
        }



        public async Task<IEnumerable<SelectListItem>> GetTeamOptionsAsync()
        {
            var client = CreateClientWithToken();
            var response = await client.GetAsync("teams");
            if (!response.IsSuccessStatusCode) return Enumerable.Empty<SelectListItem>();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PagedResult<TeamOptionViewModel>>(json);

            return data.Items.Select(t => new SelectListItem
            {
                Value = t.TeamId,
                Text = t.Name
            });
        }

        public async Task<bool> CreateAsync(DriverFormViewModel model)
        {
            var client = CreateClientWithToken();
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync("drivers", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(DriverFormViewModel model)
        {
            var client = CreateClientWithToken();
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PutAsync($"drivers/{model.DriverId}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var client = CreateClientWithToken();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"drivers/{id}");

            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
