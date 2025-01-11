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

        public async Task<string> ChangeEmailAsync(string oldEmail, string newEmail)
        {
            var endpoint = _endpointURL["ChangeEmail"];
            var requestBody = new { OldEmail = oldEmail, NewEmail = newEmail };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ChangeNameAsync(string oldName, string newName)
        {
            var endpoint = _endpointURL["ChangeName"];
            var requestBody = new { OldName = oldName, NewName = newName };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ChangePasswordAsync(string oldPasswordHash, string newPasswordHash)
        {
            var endpoint = _endpointURL["ChangePassword"];
            var requestBody = new { OldPasswordHash = oldPasswordHash, NewPasswordHash = newPasswordHash };
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
