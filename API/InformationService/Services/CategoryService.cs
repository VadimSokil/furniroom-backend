using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public CategoryService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var categories = new List<CategoryModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllCategory"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new CategoryModel
                            {
                                category_id = reader.GetInt32("category_id"),
                                category_name = reader.GetString("category_name")
                            });
                        }
                    }
                }
            }

            return categories;

        }
    }
}
