﻿using AccountsService.Models.Response;
using FurniroomAPI.Interfaces;
using System.Text.Json;

namespace FurniroomAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _endpointURL;

        public CompanyService(HttpClient httpClient, Dictionary<string, string> endpointURL)
        {
            _httpClient = httpClient;
            _endpointURL = endpointURL;
        }

        public async Task<ServiceResponseModel> GetCompanyInformationAsync()
        {
            return await GetInformationAsync("GetCompanyInformation");
        }

        public async Task<ServiceResponseModel> GetDeliveryInformationAsync()
        {
            return await GetInformationAsync("GetDeliveryInformation");
        }

        public async Task<ServiceResponseModel> GetPaymentInformationAsync()
        {
            return await GetInformationAsync("GetPaymentInformation");
        }

        private async Task<ServiceResponseModel> GetInformationAsync(string endpointKey)
        {
            try
            {
                var endpoint = _endpointURL[endpointKey];
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeResponse(responseBody);
            }
            catch (HttpRequestException httpEx)
            {
                return CreateErrorResponse($"HTTP request error: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                return CreateErrorResponse($"Error parsing service response: {jsonEx.Message}");
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
