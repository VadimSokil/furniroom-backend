using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public CategoryService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllCategories()
        {
            var categoryEndpoint = _endpointURL["GetCategoriesURL"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
