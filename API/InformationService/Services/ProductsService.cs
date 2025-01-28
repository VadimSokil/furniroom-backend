using InformationService.Interfaces;
using InformationService.Models.Products;
using InformationService.Models.Response;
using MySql.Data.MySqlClient;

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
            return await GetInformationAsync<CategoryModel>("GetAllCategories", reader => new CategoryModel
            {
                CategoryId = reader.GetInt32("CategoryId"),
                CategoryName = reader.GetString("CategoryName")
            });
        }

        public async Task<ServiceResponseModel> GetAllSubcategoriesAsync()
        {
            return await GetInformationAsync<SubcategoryModel>("GetAllSubcategories", reader => new SubcategoryModel
            {
                SubcategoryId = reader.GetInt32("SubcategoryId"),
                CategoryId = reader.GetInt32("CategoryId"),
                SubcategoryName = reader.GetString("SubcategoryName")
            });
        }

        public async Task<ServiceResponseModel> GetAllProductsAsync()
        {
            return await GetInformationAsync<ProductModel>("GetAllProducts", reader => new ProductModel
            {
                ProductId = reader.GetInt32("ProductId"),
                SubcategoryId = reader.GetInt32("SubcategoryId"),
                ProductName = reader.GetString("ProductName"),
                ProductDescription = reader.GetString("ProductDescription"),
                ProductImageUrl = reader.GetString("ProductImageUrl")
            });
        }

        public async Task<ServiceResponseModel> GetAllImagesAsync()
        {
            return await GetInformationAsync<ImageModel>("GetAllImages", reader => new ImageModel
            {
                ImageId = reader.GetInt32("ImageId"),
                ProductId = reader.GetInt32("ProductId"),
                ImageUrl = reader.GetString("ImageUrl")
            });
        }

        public async Task<ServiceResponseModel> GetAllModulesAsync()
        {
            return await GetInformationAsync<ModuleModel>("GetAllModules", reader => new ModuleModel
            {
                ModuleId = reader.GetInt32("ModuleId"),
                ProductId = reader.GetInt32("ProductId"),
                ModuleName = reader.GetString("ModuleName"),
                ModuleDescription = reader.GetString("ModuleDescription"),
                ModuleImageUrl = reader.GetString("ModuleImageUrl")
            });
        }

        private async Task<ServiceResponseModel> GetInformationAsync<T>(string requestKey, Func<MySqlDataReader, T> mapFunc)
        {
            var items = new List<T>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests[requestKey], connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                items.Add(mapFunc((MySqlDataReader)reader));
                            }
                        }
                    }
                }

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Data retrieved successfully.",
                    Data = items
                };
            }
            catch (MySqlException ex)
            {
                return CreateErrorResponse($"A database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private ServiceResponseModel CreateErrorResponse(string message)
        {
            return new ServiceResponseModel
            {
                Status = false,
                Message = message
            };
        }
    }
}
