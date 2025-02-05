using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public ProductsService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> GetAllCategoriesAsync(string requestId)
        {
            return await GetInformationAsync("GetAllCategories", requestId);
        }

        public async Task<ServiceResponseModel> GetAllModulesAsync(string requestId)
        {
            return await GetInformationAsync("GetAllModules", requestId);
        }

        public async Task<ServiceResponseModel> GetAllImagesAsync(string requestId)
        {
            return await GetInformationAsync("GetAllImages", requestId);
        }

        public async Task<ServiceResponseModel> GetAllProductsAsync(string requestId)
        {
            return await GetInformationAsync("GetAllProducts", requestId);
        }

        public async Task<ServiceResponseModel> GetAllSubcategoriesAsync(string requestId)
        {
            return await GetInformationAsync("GetAllSubcategories", requestId);
        }

        public async Task<ServiceResponseModel> GetAllSizesAsync(string requestId)
        {
            return await GetInformationAsync("GetAllSizes", requestId);
        }

        public async Task<ServiceResponseModel> GetAllColorsAsync(string requestId)
        {
            return await GetInformationAsync("GetAllColors", requestId);
        }

        private async Task<ServiceResponseModel> GetInformationAsync(string endpointKey, string requestId)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Идет выполнение запроса к {endpoint}.");

                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Запрос к {endpoint} выполнен успешно.");

                return DeserializeResponse(responseBody, requestId);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Возникла ошибка HTTP: {httpEx.Message}");
                return CreateErrorResponse($"HTTP request error: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Возникла ошибка десериализации: {jsonEx.Message}");
                return CreateErrorResponse($"Error parsing service response: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Возникла непредвиденная ошибка: {ex.Message}");
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