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
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public ProductsService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> GetAllCategoriesAsync()
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully.",
                    Data = categories
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> GetAllSubcategoriesAsync()
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully.",
                    Data = subcategories
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> GetAllProductsAsync()
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully.",
                    Data = products
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
        public async Task<ResponseModel> GetAllImagesAsync()
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully.",
                    Data = images
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> GetAllDrawingsAsync()
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Data retrieved successfully.",
                    Data = drawings
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
    }
}
