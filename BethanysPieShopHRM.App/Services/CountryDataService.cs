using BethanysPieShopHRM.Shared.Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace BethanysPieShopHRM.App.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;

        public CountryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Country>>("api/country",
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await _httpClient.GetFromJsonAsync<Country>($"api/country/{countryId}",
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
