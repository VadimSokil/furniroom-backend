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
            return await GetInformationAsync("CheckEmail", email);
        }

        public async Task<ServiceResponseModel> GenerateCodeAsync(string email)
        {
            return await GetInformationAsync("GenerateCode", email);
        }

        public async Task<ServiceResponseModel> LoginAsync(LoginModel login)
        {
            return await PostInformationAsync("Login", login);
        }

        public async Task<ServiceResponseModel> RegisterAsync(RegisterModel register)
        {
            return await PostInformationAsync("Register", register);
        }

        public async Task<ServiceResponseModel> ResetPasswordAsync(string email)
        {
            return await PostInformationAsync("ResetPassword", email);
        }

        private async Task<ServiceResponseModel> GetInformationAsync(string endpointKey, string parameter)
        {
            try
            {
                var endpoint = $"{_endpointURL[endpointKey]}?email={Uri.EscapeDataString(parameter)}";
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeResponse(responseBody);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is JsonException)
            {
                return CreateErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private async Task<ServiceResponseModel> PostInformationAsync<T>(string endpointKey, T model)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeResponse(responseBody);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is JsonException)
            {
                return CreateErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private ServiceResponseModel DeserializeResponse(string responseBody)
        {
            var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (serviceResponse?.Status == null)
            {
                return CreateErrorResponse("The data transmitted by the service to the gateway is in an incorrect format");
            }

            return serviceResponse;
        }

        private ServiceResponseModel CreateErrorResponse(string message)
        {
            return new ServiceResponseModel
            {
                Status = false,
                Message = message
            };
        }
    }
}
