using InformationService.Interfaces;
using InformationService.Models.Company;
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

        public async Task<List<CompanyModel>> GetCompanyInformationAsync()
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

            return notes;

        }

        public async Task<List<CompanyModel>> GetDeliveryInformationAsync()
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

            return notes;
        }

        public async Task<List<CompanyModel>> GetPaymentInfromationAsync()
        {
            var notes = new List<CompanyModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["GetPaymentInfromation"], connection))
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

            return notes;
        }
    }
}
