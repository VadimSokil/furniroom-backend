using AccountsService.Models.Account;
using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public AccountService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail)
        {
            return await PutDataAsync("ChangeEmail", changeEmail);
        }

        public async Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName)
        {
            return await PutDataAsync("ChangeName", changeName);
        }

        public async Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            return await PutDataAsync("ChangePassword", changePassword);
        }

        public async Task<ServiceResponseModel> DeleteAccountAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["DeleteAccount"]}/{accountId}";
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ServiceResponseModel>(responseBody) ??
                   new ServiceResponseModel { Status = false, Message = "Invalid response format." };
        }

        public async Task<ServiceResponseModel> GetAccountInformationAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["GetAccountInformation"]}/{accountId}";
            return await GetDataAsync(endpoint);
        }

        public async Task<ServiceResponseModel> GetAccountOrdersAsync(int accountId)
        {
            var endpoint = $"{_endpointURL["GetAccountOrders"]}/{accountId}";
            return await GetDataAsync(endpoint);
        }

        private async Task<ServiceResponseModel> GetDataAsync(string endpointKey)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (serviceResponse?.Status == null)
                {
                    return new ServiceResponseModel
                    {
                        Status = false,
                        Message = "The data transmitted by the service to the gateway is in an incorrect format"
                    };
                }

                return serviceResponse;
            }
            catch (HttpRequestException httpEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"HTTP request error: {httpEx.Message}"
                };
            }
            catch (JsonException jsonEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"Error parsing service response: {jsonEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        private async Task<ServiceResponseModel> PutDataAsync<T>(string endpointKey, T data)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var requestBody = JsonSerializer.Serialize(data);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (serviceResponse?.Status == null)
                {
                    return new ServiceResponseModel
                    {
                        Status = false,
                        Message = "The data transmitted by the service to the gateway is in an incorrect format"
                    };
                }

                return serviceResponse;
            }
            catch (HttpRequestException httpEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"HTTP request error: {httpEx.Message}"
                };
            }
            catch (JsonException jsonEx)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"Error parsing service response: {jsonEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
    }
}
