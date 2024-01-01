using BethanysPieShopHRM.Shared.Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace BethanysPieShopHRM.App.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _httpClient;

        public JobCategoryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<JobCategory>>("api/jobcategory",
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId)
        {
            return await _httpClient.GetFromJsonAsync<JobCategory>($"api/jobcategory/{jobCategoryId}",
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
