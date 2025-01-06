using InformationService.Interfaces;
using InformationService.Models.Products;
using MySql.Data.MySqlClient;
using System.Data;

namespace InformationService.Services
{
    public class ProductsService : IProductsService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public ProductsService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            var categories = new List<CategoryModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["GetAllCategories"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new CategoryModel
                            {
                                CategoryId = reader.GetInt32("CategoryId"),
                                CategoryName = reader.GetString("CategoryName")
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public async Task<List<SubcategoryModel>> GetAllSubcategoriesAsync()
        {
            var subcategories = new List<SubcategoryModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["GetAllSubcategories"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subcategories.Add(new SubcategoryModel
                            {
                                SubcategoryId = reader.GetInt32("SubcategoryId"),
                                CategoryId = reader.GetInt32("CategoryId"),
                                SubcategoryName = reader.GetString("SubcategoryName")
                            });
                        }
                    }
                }
            }

            return subcategories;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var products = new List<ProductModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["GetAllProducts"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new ProductModel
                            {
                                ProductId = reader.GetInt32("ProductId"),
                                SubcategoryId = reader.GetInt32("SubcategoryId"),
                                ProductName = reader.GetString("ProductName"),
                                ProductDescription = reader.GetString("ProductDescription"),
                                ProductImageUrl = reader.GetString("ProductImageUrl")
                            });
                        }
                    }
                }
            }

            return products;
        }
        public async Task<List<ImageModel>> GetAllImagesAsync()
        {
            var images = new List<ImageModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["GetAllImages"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            images.Add(new ImageModel
                            {
                                ImageId = reader.GetInt32("ImageId"),
                                ProductId = reader.GetInt32("ProductId"),
                                ImageUrl = reader.GetString("ImageUrl")
                            });
                        }
                    }
                }
            }

            return images;
        }

        public async Task<List<DrawingModel>> GetAllDrawingsAsync()
        {
            var drawingss = new List<DrawingModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["GetAllSubcategories"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            drawingss.Add(new DrawingModel
                            {
                                DrawingId = reader.GetInt32("DrawingId"),
                                ProductId = reader.GetInt32("ProductId"),
                                DrawingName = reader.GetString("DrawingName"),
                                DrawingDescription = reader.GetString("DrawingDescription"),
                                DrawingImageUrl = reader.GetString("DrawingImageUrl")
                            });
                        }
                    }
                }
            }

            return drawingss;
        }
    }
}
