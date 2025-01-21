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
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> GetAllDrawingsAsync()
        {
            var endpoint = _endpointURL["GetAllDrawings"];
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> GetAllImagesAsync()
        {
            var endpoint = _endpointURL["GetAllImages"];
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> GetAllProductsAsync()
        {
            var endpoint = _endpointURL["GetAllProducts"];
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> GetAllSubcategoriesAsync()
        {
            var endpoint = _endpointURL["GetAllSubcategories"];
            return await GetDataAsync(endpoint);
        }

        private async Task<ServiceResponseModel> GetDataAsync(string endpoint)
        {
            try
            {
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
    }
}
