using InformationService.Interfaces;
using InformationService.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class ProductGalleryService : IProductGalleryService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public ProductGalleryService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<ProductGalleryModel>> GetAllProductGallery()
        {
            var gallery = new List<ProductGalleryModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetProductGallery"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            gallery.Add(new ProductGalleryModel
                            {
                                product_id = reader.GetInt32("product_id"),
                                img_id = reader.GetInt32("img_id"),
                                img_url = reader.GetString("img_url")
                            });
                        }
                    }
                }
            }

            return gallery;

        }
    }
}
