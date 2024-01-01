using BethanysPieShopHRM.App.Helper;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace BethanysPieShopHRM.App.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public EmployeeDataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employee", employee);
            if (response.IsSuccessStatusCode)
            {
                if (await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey))
                {
                    await _localStorageService.RemoveItemAsync(LocalStorageConstants.EmployeesListKey);
                }

                return await response.Content.ReadFromJsonAsync<Employee>();
            }

            return null;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _httpClient.DeleteAsync($"api/employee/{employeeId}");

            if (await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey))
            {
                await _localStorageService.RemoveItemAsync(LocalStorageConstants.EmployeesListKey);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(bool refreshRequired = false)
        {
            if (!refreshRequired)
            {
                bool employeeExpirationExists = await _localStorageService.ContainKeyAsync(
                    LocalStorageConstants.EmployeesListExpirationKey);

                if (employeeExpirationExists)
                {
                    DateTime employeeListExpiration = await _localStorageService.GetItemAsync<DateTime>(
                        LocalStorageConstants.EmployeesListExpirationKey);

                    if (employeeListExpiration > DateTime.Now &&
                        await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey))
                    {
                        return await _localStorageService.GetItemAsync<List<Employee>>(
                            LocalStorageConstants.EmployeesListKey);
                    }
                }
            }

            var list = await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>("api/employee");

            await _localStorageService.SetItemAsync(LocalStorageConstants.EmployeesListKey, list);
            await _localStorageService.SetItemAsync(
                LocalStorageConstants.EmployeesListExpirationKey, DateTime.Now.AddMinutes(1));

            return list;
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"api/employee/{employeeId}");
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _httpClient.PutAsJsonAsync("api/employee", employee);

            if (await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey))
            {
                await _localStorageService.RemoveItemAsync(LocalStorageConstants.EmployeesListKey);
            }
        }
    }
}
