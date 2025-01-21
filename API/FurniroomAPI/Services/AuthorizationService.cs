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
            try
            {
                var endpoint = $"{_endpointURL["CheckEmail"]}?email={Uri.EscapeDataString(email)}";
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

        public async Task<ServiceResponseModel> GenerateCodeAsync(string email)
        {
            try
            {
                var endpoint = $"{_endpointURL["GenerateCode"]}?email={Uri.EscapeDataString(email)}";
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

        public async Task<ServiceResponseModel> LoginAsync(LoginModel login)
        {
            try
            {
                var endpoint = _endpointURL["Login"];
                var jsonContent = JsonSerializer.Serialize(login);
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

        public async Task<ServiceResponseModel> RegisterAsync(RegisterModel register)
        {
            try
            {
                var endpoint = _endpointURL["Register"];
                var jsonContent = JsonSerializer.Serialize(register);
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

        public async Task<ServiceResponseModel> ResetPasswordAsync(string email)
        {
            try
            {
                var endpoint = _endpointURL["ResetPassword"];
                var jsonContent = JsonSerializer.Serialize(email);
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
