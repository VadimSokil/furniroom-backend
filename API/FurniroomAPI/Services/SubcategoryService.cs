using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public SubcategoryService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAllSubcategories()
        {
            var categoryEndpoint = _endpointURL["GetSubcategoriesURL"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
