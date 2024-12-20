using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public PaymentService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllPaymentInfo()
        {
            var categoryEndpoint = _endpointURL["GetPaymentInfo"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
