using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public CompanyService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> GetCompanyInformationAsync()
        {
            return await FetchDataAsync("GetCompanyInformation");
        }

        public async Task<ServiceResponseModel> GetDeliveryInformationAsync()
        {
            return await FetchDataAsync("GetDeliveryInformation");
        }

        public async Task<ServiceResponseModel> GetPaymentInformationAsync()
        {
            return await FetchDataAsync("GetPaymentInformation");
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
