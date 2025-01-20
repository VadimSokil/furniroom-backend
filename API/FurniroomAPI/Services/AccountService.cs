using AccountsService.Models.Account;
using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public AccountService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail)
        {
            var endpoint = _endpointURL["ChangeEmail"];
            var requestBody = JsonSerializer.Serialize(changeEmail);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName)
        {
            var endpoint = _endpointURL["ChangeName"];
            var requestBody = JsonSerializer.Serialize(changeName);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            var endpoint = _endpointURL["ChangePassword"];
            var requestBody = JsonSerializer.Serialize(changePassword);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> DeleteAccountAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["DeleteAccount"]}/{accountId}";
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetAccountInformationAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["GetAccountInformation"]}/{accountId}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetAccountOrdersAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["GetAccountOrders"]}/{accountId}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }
    }
}
