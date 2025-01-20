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
            return await FetchDataAsync("GetAllCategories");
        }

        public async Task<ServiceResponseModel> GetAllDrawingsAsync()
        {
            return await FetchDataAsync("GetAllDrawings");
        }

        public async Task<ServiceResponseModel> GetAllImagesAsync()
        {
            return await FetchDataAsync("GetAllImages");
        }

        public async Task<ServiceResponseModel> GetAllProductsAsync()
        {
            return await FetchDataAsync("GetAllProducts");
        }

        public async Task<ServiceResponseModel> GetAllSubcategoriesAsync()
        {
            return await FetchDataAsync("GetAllSubcategories");
        }

        private async Task<ServiceResponseModel> FetchDataAsync(string endpointKey)
        {
            try
            {
                if (!_endpointURL.TryGetValue(endpointKey, out var endpoint))
                {
                    return new ServiceResponseModel
                    {
                        Status = false,
                        Message = $"Endpoint key {endpointKey} not found in configuration."
                    };
                }

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
