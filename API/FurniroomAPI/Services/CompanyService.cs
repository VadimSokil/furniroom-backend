using FurniroomAPI.Interfaces;

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

        public async Task<string> GetCompanyInformationAsync()
        {
            var endpoint = _endpointURL["GetCompanyInformation"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetDeliveryInformationAsync()
        {
            var endpoint = _endpointURL["GetDeliveryInformation"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPaymentInformationAsync()
        {
            var endpoint = _endpointURL["GetPaymentInformation"];
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
