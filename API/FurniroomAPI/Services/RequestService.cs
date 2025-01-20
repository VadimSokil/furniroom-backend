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
            var endpoint = _endpointURL["AddOrder"];
            var jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> AddQuestionAsync(QuestionModel question)
        {
            var endpoint = _endpointURL["AddQuestion"];
            var jsonContent = JsonSerializer.Serialize(question);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }
    }
}
