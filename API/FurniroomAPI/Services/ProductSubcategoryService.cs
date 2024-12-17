using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class ProductSubcategoryService : IProductSubcategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public ProductSubcategoryService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllProductSubcategory()
        {
            var categoryEndpoint = _endpointURL["GetProductSubcategoriesURL"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
