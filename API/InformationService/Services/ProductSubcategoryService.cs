using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class ProductSubcategoryService : IProductSubcategoryService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public ProductSubcategoryService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<ProductSubcategoryModel>> GetAllProductSubcategories()
        {
            var productsubcategory = new List<ProductSubcategoryModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllProductSubcategory"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productsubcategory.Add(new ProductSubcategoryModel
                            {
                                subcategory_id = reader.GetInt32("subcategory_id"),
                                product_id = reader.GetInt32("product_id"),
                                product_subcategory_id = reader.GetInt32("product_subcategory_id")
                            });
                        }
                    }
                }
            }

            return productsubcategory;

        }
    }
}
