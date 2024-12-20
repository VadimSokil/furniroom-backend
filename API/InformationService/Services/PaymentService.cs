using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public PaymentService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<DeliveryPaymentsModel>> GetAllPaymentsInfo()
        {
            var notes = new List<DeliveryPaymentsModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllPaymentInfo"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            notes.Add(new DeliveryPaymentsModel
                            {
                                note_id = reader.GetInt32("note_id"),
                                note = reader.GetString("note")
                            });
                        }
                    }
                }
            }

            return notes;

        }
    }
}
