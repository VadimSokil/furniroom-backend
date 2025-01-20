using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public ProductsService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> GetAllCategoriesAsync()
        {
            var endpoint = _endpointURL["GetAllCategories"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {responseBody}");

            var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (serviceResponse == null)
            {
                Console.WriteLine("Deserialization failed.");
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = "Invalid response format.",
                    Data = responseBody // Сохранение исходного тела ответа в поле Data для отладки
                };
            }

            Console.WriteLine($"Deserialized Response: Status = {serviceResponse.Status}, Message = {serviceResponse.Message}, Data = {serviceResponse.Data}");

            return serviceResponse;
        }




        public async Task<ServiceResponseModel> GetAllDrawingsAsync()
        {
            var endpoint = _endpointURL["GetAllDrawings"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetAllImagesAsync()
        {
            var endpoint = _endpointURL["GetAllImages"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetAllProductsAsync()
        {
            var endpoint = _endpointURL["GetAllProducts"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetAllSubcategoriesAsync()
        {
            var endpoint = _endpointURL["GetAllSubcategories"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }
    }
}
