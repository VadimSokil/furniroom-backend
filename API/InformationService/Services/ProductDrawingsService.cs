using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class ProductDrawingsService : IProductDrawingsService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public ProductDrawingsService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<ProductDrawingsModel>> GetAllProductDrawings()
        {
            var drawings = new List<ProductDrawingsModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetProductDrawings"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            drawings.Add(new ProductDrawingsModel
                            {
                                product_id = reader.GetInt32("product_id"),
                                drawing_id = reader.GetInt32("drawing_id"),
                                drawing_name = reader.GetString("drawing_name"),
                                drawing_description = reader.GetString("drawing_description"),
                                drawing_img_url = reader.GetString("drawing_img_url")
                            });
                        }
                    }
                }
            }

            return drawings;

        }
    }
}
