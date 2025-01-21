using InformationService.Interfaces;
using InformationService.Models.Products;
using InformationService.Models.Response;
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

        public async Task<ServiceResponseModel> GetAllCategoriesAsync()
        {
            try
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

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = categories
                };
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponseModel> GetAllSubcategoriesAsync()
        {
            try
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

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = subcategories
                };
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponseModel> GetAllProductsAsync()
        {
            try
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

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = products
                };
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
        public async Task<ServiceResponseModel> GetAllImagesAsync()
        {
            try
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

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = images
                };
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponseModel> GetAllDrawingsAsync()
        {
            try
            {
                var drawings = new List<DrawingModel>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["GetAllDrawings"], connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                drawings.Add(new DrawingModel
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

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = drawings
                };
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
    }
}
