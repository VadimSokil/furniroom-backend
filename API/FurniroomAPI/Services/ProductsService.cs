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
            // Получаем endpoint из конфигурации
            var endpoint = _endpointURL["GetAllCategories"];

            // Отправляем запрос
            var response = await _httpClient.GetAsync(endpoint);

            // Проверяем успешность запроса
            response.EnsureSuccessStatusCode();

            // Читаем тело ответа
            var responseBody = await response.Content.ReadAsStringAsync();

            // Отладочное сообщение: выводим сырой ответ от сервиса
            Console.WriteLine($"Response Body: {responseBody}");

            // Пытаемся десериализовать тело в объект
            var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody);

            // Отладочное сообщение: выводим результат десериализации
            if (serviceResponse != null)
            {
                Console.WriteLine($"Deserialized Response: Status = {serviceResponse.Status}, Message = {serviceResponse.Message}, Data = {serviceResponse.Data}");
            }
            else
            {
                Console.WriteLine("Failed to deserialize response.");
            }

            // Если десериализация не удалась, возвращаем дефолтные данные с ошибкой
            return serviceResponse ?? new ServiceResponseModel
            {
                Status = false,
                Message = "Invalid response format.",
                Data = new object[] { responseBody } // Можете сохранить исходный ответ, если десериализация не удалась
            };
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
