using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text;
using System.Text.Json;
using static FurniroomAPI.Models.AuthorizationModels;

namespace FurniroomAPI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public AuthorizationService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> CheckEmailAsync(EmailModel email, string requestId)
        {
            return await GetInformationAsync("CheckEmail", email.Email, requestId);
        }

        public async Task<ServiceResponseModel> GenerateCodeAsync(EmailModel email, string requestId)
        {
            return await GetInformationAsync("GenerateCode", email.Email, requestId);
        }

        public async Task<ServiceResponseModel> LoginAsync(LoginModel login, string requestId)
        {
            return await PostInformationAsync("Login", login, requestId);
        }

        public async Task<ServiceResponseModel> RegisterAsync(RegisterModel register, string requestId)
        {
            return await PostInformationAsync("Register", register, requestId);
        }

        public async Task<ServiceResponseModel> ResetPasswordAsync(EmailModel email, string requestId)
        {
            return await PostInformationAsync("ResetPassword", email.Email, requestId);
        }

        private async Task<ServiceResponseModel> GetInformationAsync(string endpointKey, string parameter, string requestId)
        {
            try
            {
                var endpoint = $"{_endpointURL[endpointKey]}?email={Uri.EscapeDataString(parameter)}";
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Идет выполнение запроса к {endpoint}.");

                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Запрос к {endpoint} выполнен успешно.");

                return DeserializeResponse(responseBody, requestId);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка HTTP-запроса: {httpEx.Message}");
                return CreateErrorResponse($"HTTP request error: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка десериализации: {jsonEx.Message}");
                return CreateErrorResponse($"Error parsing service response: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Непредвиденная ошибка: {ex.Message}");
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private async Task<ServiceResponseModel> PostInformationAsync<T>(string endpointKey, T model, string requestId)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Идет выполнение запроса к {endpoint}.");

                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Запрос к {endpoint} выполнен успешно.");

                return DeserializeResponse(responseBody, requestId);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка HTTP-запроса: {httpEx.Message}");
                return CreateErrorResponse($"HTTP request error: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка десериализации: {jsonEx.Message}");
                return CreateErrorResponse($"Error parsing service response: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Непредвиденная ошибка: {ex.Message}");
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private ServiceResponseModel DeserializeResponse(string responseBody, string requestId)
        {
            try
            {
                var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (serviceResponse?.Status == null)
                {
                    Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Некорректный формат данных от сервиса.");
                    return CreateErrorResponse("The data transmitted by the service to the gateway is in an incorrect format");
                }

                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Успешная десериализация ответа.");
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка десериализации: {ex.Message}");
                return CreateErrorResponse($"Error deserializing response: {ex.Message}");
            }
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