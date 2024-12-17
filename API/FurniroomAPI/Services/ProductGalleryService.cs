using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class ProductGalleryService : IProductGalleryService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public ProductGalleryService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllProductGallery()
        {
            var categoryEndpoint = _endpointURL["GetProductGalleryURL"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
