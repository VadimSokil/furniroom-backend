using FurniroomAPI.Interfaces;

namespace FurniroomAPI.Services
{
    public class AboutCompanyService : IAboutCompanyService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public AboutCompanyService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<string> GetAboutCompany()
        {
            var categoryEndpoint = _endpointURL["GetAboutCompany"];
            var response = await _httpClient.GetAsync(categoryEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
