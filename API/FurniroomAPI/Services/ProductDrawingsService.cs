using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class ProductDrawingsService : IProductDrawingsService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public ProductDrawingsService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllProductDrawings()
        {
            var categoryEndpoint = _endpointURL["GetProductDrawingsURL"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
