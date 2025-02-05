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
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public AccountService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail, string requestId)
        {
            return await PutInformationAsync("ChangeEmail", changeEmail, requestId);
        }

        public async Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName, string requestId)
        {
            return await PutInformationAsync("ChangeName", changeName, requestId);
        }

        public async Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword, string requestId)
        {
            return await PutInformationAsync("ChangePassword", changePassword, requestId);
        }

        public async Task<ServiceResponseModel> DeleteAccountAsync(int accountId, string requestId)
        {
            return await DeleteInformationAsync("DeleteAccount", accountId, requestId);
        }

        public async Task<ServiceResponseModel> GetAccountInformationAsync(int accountId, string requestId)
        {
            return await GetInformationAsync("GetAccountInformation", accountId, requestId);
        }

        public async Task<ServiceResponseModel> GetAccountOrdersAsync(int accountId, string requestId)
        {
            return await GetInformationAsync("GetAccountOrders", accountId, requestId);
        }

        private async Task<ServiceResponseModel> PutInformationAsync<T>(string endpointKey, T model, string requestId)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Идет выполнение запроса к {endpoint}.");

                var requestBody = JsonSerializer.Serialize(model);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);
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

        private async Task<ServiceResponseModel> DeleteInformationAsync(string endpointKey, int parameter, string requestId)
        {
            try
            {
                var endpoint = $"{_endpointURL[endpointKey]}?accountId={Uri.EscapeDataString(parameter.ToString())}";
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Идет выполнение запроса к {endpoint}.");

                var response = await _httpClient.DeleteAsync(endpoint);
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

        private async Task<ServiceResponseModel> GetInformationAsync(string endpointKey, int parameter, string requestId)
        {
            try
            {
                var endpoint = $"{_endpointURL[endpointKey]}?accountId={Uri.EscapeDataString(parameter.ToString())}";
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