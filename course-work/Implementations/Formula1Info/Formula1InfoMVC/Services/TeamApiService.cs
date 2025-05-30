using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Team;
using Formula1InfoMVC.ViewModels.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Formula1InfoMVC.Services
{
    public class TeamApiService : ITeamApiService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _httpContext;

        public TeamApiService(IHttpClientFactory factory, IHttpContextAccessor httpContext)
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

        public async Task<PagedResult<TeamViewModel>> GetAllAsync(string? name, string? country, int page = 1, int pageSize = 5)
        {
            var client = CreateClientWithToken();

            var query = $"teams?name={name}&country={country}&page={page}&pageSize={pageSize}";
            var response = await client.GetAsync(query);

            if (!response.IsSuccessStatusCode)
                return new PagedResult<TeamViewModel>
                {
                    Items = new List<TeamViewModel>(),
                    TotalCount = 0,
                    TotalPages = 0
                };

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PagedResult<TeamViewModel>>(json);
        }


        public async Task<TeamFormViewModel?> GetByIdAsync(string id)
        {
            var client = CreateClientWithToken();
            var response = await client.GetAsync($"teams/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TeamFormViewModel>(json);
        }

        public async Task<bool> CreateAsync(TeamFormViewModel model)
        {
            var client = CreateClientWithToken();
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync("teams", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(TeamFormViewModel model)
        {
            var client = CreateClientWithToken();
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PutAsync($"teams/{model.TeamId}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var client = CreateClientWithToken();
            var response = await client.DeleteAsync($"teams/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
