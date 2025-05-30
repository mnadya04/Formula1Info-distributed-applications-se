using Formula1InfoMVC.Services.Interfaces;
using Formula1InfoMVC.ViewModels.Helpers;
using Formula1InfoMVC.ViewModels.Race;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Formula1InfoMVC.Services
{
    public class RaceApiService : IRaceApiService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _context;

        public RaceApiService(IHttpClientFactory factory, IHttpContextAccessor context)
        {
            _factory = factory;
            _context = context;
        }

        private HttpClient CreateClientWithToken()
        {
            var client = _factory.CreateClient("ApiClient");
            var token = _context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public async Task<PagedResult<RaceViewModel>> GetAllAsync(string? name, bool? isFuture, int page = 1, int pageSize = 5)
        {
            var client = CreateClientWithToken();
            var query = $"races?name={name}&isFuture={isFuture}&page={page}&pageSize={pageSize}";
            var response = await client.GetAsync(query);

            if (!response.IsSuccessStatusCode)
                return new PagedResult<RaceViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PagedResult<RaceViewModel>>(json)!;
        }

        public async Task<RaceFormViewModel?> GetByIdAsync(string id)
        {
            var client = CreateClientWithToken();
            var response = await client.GetAsync($"races/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RaceFormViewModel>(json);
        }

        public async Task<IEnumerable<SelectListItem>> GetDriverOptionsAsync()
        {
            var client = CreateClientWithToken();
            var response = await client.GetAsync("drivers");
            if (!response.IsSuccessStatusCode) return Enumerable.Empty<SelectListItem>();

            var json = await response.Content.ReadAsStringAsync();
            var drivers = JsonConvert.DeserializeObject<PagedResult<DriverOptionViewModel>>(json)!;

            return drivers.Items.Select(d => new SelectListItem
            {
                Value = d.DriverId,
                Text = d.FullName
            });
        }

        public async Task<bool> CreateAsync(RaceFormViewModel model)
        {
            var client = CreateClientWithToken();
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync("races", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(RaceFormViewModel model)
        {
            var client = CreateClientWithToken();
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PutAsync($"races/{model.RaceId}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var client = CreateClientWithToken();
            var response = await client.DeleteAsync($"races/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
