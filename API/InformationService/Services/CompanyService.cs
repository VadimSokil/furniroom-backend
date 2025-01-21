using InformationService.Interfaces;
using InformationService.Models.Company;
using InformationService.Models.Response;
using MySql.Data.MySqlClient;
using System.Data;

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
        return await ExecuteQueryAsync(_requests["GetCompanyInformation"]);
    }

    public async Task<ServiceResponseModel> GetDeliveryInformationAsync()
    {
        return await ExecuteQueryAsync(_requests["GetDeliveryInformation"]);
    }

    public async Task<ServiceResponseModel> GetPaymentInformationAsync()
    {
        return await ExecuteQueryAsync(_requests["GetPaymentInformation"]);
    }

    private async Task<ServiceResponseModel> ExecuteQueryAsync(string query)
    {
        try
        {
            var notes = new List<CompanyModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(query, connection))
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
            return new ServiceResponseModel
            {
                Status = false,
                Message = $"A database error occurred: {ex.Message}"
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
