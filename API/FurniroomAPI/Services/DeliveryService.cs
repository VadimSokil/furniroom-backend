using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public DeliveryService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllDeliveryInfo()
        {
            var categoryEndpoint = _endpointURL["GetDeliveryInfo"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
