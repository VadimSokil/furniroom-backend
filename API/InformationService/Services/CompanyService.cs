using InformationService.Interfaces;
using InformationService.Models.Company;
using InformationService.Models.Response;
using InformationService.Validation;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public ValidationMethods validationMethods = new ValidationMethods();

        public CompanyService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> GetCompanyInformationAsync()
        {
            try
            {
                var notes = new List<CompanyModel>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["GetCompanyInformation"], connection))
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully",
                    Data = notes
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }

        }

        public async Task<ResponseModel> GetDeliveryInformationAsync()
        {
            try
            {
                var notes = new List<CompanyModel>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["GetDeliveryInformation"], connection))
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully",
                    Data = notes
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> GetPaymentInformationAsync()
        {
            try
            {
                var notes = new List<CompanyModel>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["GetPaymentInformation"], connection))
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully",
                    Data = notes
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }
    }
}
