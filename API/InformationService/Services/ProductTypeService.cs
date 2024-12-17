using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public ProductTypeService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<ProductTypeModel>> GetAllProducts()
        {
            var products = new List<ProductTypeModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllProducts"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new ProductTypeModel
                            {
                                product_id = reader.GetInt32("product_id"),
                                product_name = reader.GetString("product_name"),
                                product_description = reader.GetString("product_description"),
                                product_img_url = reader.GetString("product_img_url")
                            });
                        }
                    }
                }
            }

            return products;
        }
    }
}
