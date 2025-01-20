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
            var endpoint = _endpointURL["GetCompanyInformation"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetDeliveryInformationAsync()
        {
            var endpoint = _endpointURL["GetDeliveryInformation"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetPaymentInformationAsync()
        {
            var endpoint = _endpointURL["GetPaymentInformation"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }
    }
}
