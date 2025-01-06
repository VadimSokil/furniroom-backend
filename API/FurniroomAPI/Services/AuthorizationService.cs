using FurniroomAPI.Interfaces;
using FurniroomAPI.Models;
using Newtonsoft.Json;
using System.Text;

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

        public async Task AddNewUser(RegisterModel register)
        {
            var addUserEndpoint = _endpointURL["AddUser"];
            var jsonContent = JsonConvert.SerializeObject(register);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(addUserEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task CheckEmailExists(string email)
        {
            var checkEmailEndpoint = _endpointURL["checkEmail"];
            var jsonContent = JsonConvert.SerializeObject(email);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(checkEmailEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task GenerateVerificationCode(string email)
        {
            var generateCodeEndpoint = _endpointURL["generateCode"];
            var jsonContent = JsonConvert.SerializeObject(email);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(generateCodeEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task Login(LoginModel login)
        {
            var loginEndpoint = _endpointURL["Login"];
            var jsonContent = JsonConvert.SerializeObject(login);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(loginEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ResetPassword(string email)
        {
            var resetPasswordEndpoint = _endpointURL["resetPassword"];
            var jsonContent = JsonConvert.SerializeObject(email);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(resetPasswordEndpoint, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
