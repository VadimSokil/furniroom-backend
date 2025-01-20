using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Request;
using System.Text;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public RequestService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> AddOrderAsync(OrderModel order)
        {
            return await PostDataAsync("AddOrder", order);
        }

        public async Task<ServiceResponseModel> AddQuestionAsync(QuestionModel question)
        {
            return await PostDataAsync("AddQuestion", question);
        }

        private async Task<ServiceResponseModel> PostDataAsync<T>(string endpointKey, T data)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var jsonContent = JsonSerializer.Serialize(data);
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
