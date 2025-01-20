using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Authorization;
using System.Text;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public AuthorizationService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> CheckEmailAsync(string email)
        {
            var endpoint = $"{_endpointURL["CheckEmail"]}?email={Uri.EscapeDataString(email)}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GenerateCodeAsync(string email)
        {
            var endpoint = $"{_endpointURL["GenerateCode"]}?email={Uri.EscapeDataString(email)}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }


        public async Task<ServiceResponseModel> LoginAsync(LoginModel login)
        {
            var endpoint = _endpointURL["Login"];
            var jsonContent = JsonSerializer.Serialize(login);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> RegisterAsync(RegisterModel register)
        {
            var endpoint = _endpointURL["Register"];
            var jsonContent = JsonSerializer.Serialize(register);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> ResetPasswordAsync(string email)
        {
            var endpoint = _endpointURL["ResetPassword"];
            var jsonContent = JsonSerializer.Serialize(email);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }
    }
}
