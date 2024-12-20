using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public DeliveryService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<DeliveryPaymentsModel>> GetAllDeliveryInfo()
        {
            var notes = new List<DeliveryPaymentsModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllDeliveryInfo"], connection))
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
