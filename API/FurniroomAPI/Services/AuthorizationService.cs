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
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> GenerateCodeAsync(string email)
        {
            var endpoint = $"{_endpointURL["GenerateCode"]}?email={Uri.EscapeDataString(email)}";
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> LoginAsync(LoginModel login)
        {
            return await PostDataAsync("Login", login);
        }

        public async Task<ServiceResponseModel> RegisterAsync(RegisterModel register)
        {
            return await PostDataAsync("Register", register);
        }

        public async Task<ServiceResponseModel> ResetPasswordAsync(string email)
        {
            return await PostDataAsync("ResetPassword", email);
        }

        private async Task<ServiceResponseModel> GetDataAsync(string endpointKey)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (serviceResponse?.Status == null)
                {
                    return new ServiceResponseModel
                    {
                        Status = false,
                        Message = "The data transmitted by the service to the gateway is in an incorrect format"
                    };
                }

                return serviceResponse;
            }
            catch (HttpRequestException httpEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"HTTP request error: {httpEx.Message}"
                };
            }
            catch (JsonException jsonEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"Error parsing service response: {jsonEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        private async Task<ServiceResponseModel> PostDataAsync<T>(string endpointKey, T data)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var jsonContent = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (serviceResponse?.Status == null)
                {
                    return new ServiceResponseModel
                    {
                        Status = false,
                        Message = "The data transmitted by the service to the gateway is in an incorrect format"
                    };
                }

                return serviceResponse;
            }
            catch (HttpRequestException httpEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"HTTP request error: {httpEx.Message}"
                };
            }
            catch (JsonException jsonEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"Error parsing service response: {jsonEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

    }
}
