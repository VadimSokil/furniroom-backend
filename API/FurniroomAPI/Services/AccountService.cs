using AccountsService.Models.Account;
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

        public async Task<string> ChangeEmailAsync(ChangeEmailModel changeEmail)
        {
            var endpoint = _endpointURL["ChangeEmail"];
            var requestBody = new { OldEmail = changeEmail.OldEmail, NewEmail = changeEmail.NewEmail };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ChangeNameAsync(ChangeNameModel changeName)
        {
            var endpoint = _endpointURL["ChangeName"];
            var requestBody = new { OldName = changeName.OldName, NewName = changeName.NewName };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            var endpoint = _endpointURL["ChangePassword"];
            var requestBody = new { OldPasswordHash = changePassword.OldPasswordHash, NewPasswordHash = changePassword.NewPasswordHash };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAccountAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["DeleteAccount"]}/{accountId}";
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountInformationAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["GetAccountInformation"]}/{accountId}"; 
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountOrdersAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["GetAccountOrders"]}/{accountId}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
