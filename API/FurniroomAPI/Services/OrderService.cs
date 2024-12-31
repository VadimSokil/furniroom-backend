using FurniroomAPI.Interfaces;
using FurniroomAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace FurniroomAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public OrderService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }
        public async Task AddOrder(OrderModel order)
        {
            var addOrderEndpoint = _endpointURL["AddOrder"];
            var jsonContent = JsonConvert.SerializeObject(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(addOrderEndpoint, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
