using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public SubcategoryService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<SubcategoryModel>> GetAllSubcategories()
        {
            var subcategories = new List<SubcategoryModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllSubcategory"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subcategories.Add(new SubcategoryModel
                            {
                                category_id = reader.GetInt32("category_id"),
                                subcategory_id = reader.GetInt32("subcategory_id"),
                                subcategory_name = reader.GetString("subcategory_name")
                            });
                        }
                    }
                }
            }

            return subcategories;

        }
    }
}
