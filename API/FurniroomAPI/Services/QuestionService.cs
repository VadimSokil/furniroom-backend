using FurniroomAPI.Interfaces;
using FurniroomAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace FurniroomAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public QuestionService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }
        public async Task AddQuestion(QuestionModel question)
        {
            var addOrderEndpoint = _endpointURL["AddQuestion"];
            var jsonContent = JsonConvert.SerializeObject(question);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(addOrderEndpoint, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
