using InformationService.Interfaces;
using InformationService.Models.Company;
using InformationService.Models.Response;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public CompanyService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
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

        private async Task<ServiceResponseModel> GetInformationAsync(string requestKey)
        {
            var notes = new List<CompanyModel>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests[requestKey], connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                notes.Add(new CompanyModel
                                {
                                    NoteId = reader.GetInt32("NoteId"),
                                    Note = reader.GetString("Note")
                                });
                            }
                        }
                    }
                }

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = notes
                };
            }
            catch (MySqlException ex)
            {
                return CreateErrorResponse($"A database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
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
