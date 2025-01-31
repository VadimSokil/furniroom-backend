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
            return await PutInformationAsync("ChangeEmail", changeEmail);
        }

        public async Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName)
        {
            return await PutInformationAsync("ChangeName", changeName);
        }

        public async Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            return await PutInformationAsync("ChangePassword", changePassword);
        }

        public async Task<ServiceResponseModel> DeleteAccountAsync(int accountId)
        {
            return await DeleteInformationAsync("DeleteAccount", accountId);
        }

        public async Task<ServiceResponseModel> GetAccountInformationAsync(int accountId)
        {
            return await GetInformationAsync("GetAccountInformation", accountId);
        }

        public async Task<ServiceResponseModel> GetAccountOrdersAsync(int accountId)
        {
            return await GetInformationAsync("GetAccountOrders", accountId);
        }

        private async Task<ServiceResponseModel> PutInformationAsync<T>(string endpointKey, T model)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var requestBody = JsonSerializer.Serialize(model);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeResponse(responseBody);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is JsonException)
            {
                return CreateErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private async Task<ServiceResponseModel> DeleteInformationAsync(string endpointKey, int parameter)
        {
            try
            {
                var endpoint = $"{_endpointURL[endpointKey]}?accountId={Uri.EscapeDataString(parameter.ToString())}";
                var response = await _httpClient.DeleteAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeResponse(responseBody);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is JsonException)
            {
                return CreateErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private async Task<ServiceResponseModel> GetInformationAsync(string endpointKey, int parameter)
        {
            try
            {
                var endpoint = $"{_endpointURL[endpointKey]}?accountId={Uri.EscapeDataString(parameter.ToString())}";
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeResponse(responseBody);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is JsonException)
            {
                return CreateErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private ServiceResponseModel DeserializeResponse(string responseBody)
        {
            var serviceResponse = JsonSerializer.Deserialize<ServiceResponseModel>(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (serviceResponse?.Status == null)
            {
                return CreateErrorResponse("The data transmitted by the service to the gateway is in an incorrect format");
            }

            return serviceResponse;
        }

        private ServiceResponseModel CreateErrorResponse(string message)
        {
            return new ServiceResponseModel
            {
                Status = false,
                Message = message
            };
        }
    }
}
