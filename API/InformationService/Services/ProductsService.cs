using InformationService.Interfaces;
using InformationService.Models.Products;
using InformationService.Models.Response;
using MySql.Data.MySqlClient;

public class ProductsService : IProductsService
{
    private readonly string _connectionString;
    private readonly Dictionary<string, string> _requests;

    public ProductsService(string connectionString, Dictionary<string, string> requests)
    {
        _connectionString = connectionString;
        _requests = requests;
    }

    public async Task<ServiceResponseModel> GetAllCategoriesAsync() =>
        await ExecuteQueryAsync<CategoryModel>(_requests["GetAllCategories"], reader => new CategoryModel
        {
            CategoryId = reader.GetInt32("CategoryId"),
            CategoryName = reader.GetString("CategoryName")
        });

    public async Task<ServiceResponseModel> GetAllSubcategoriesAsync() =>
        await ExecuteQueryAsync<SubcategoryModel>(_requests["GetAllSubcategories"], reader => new SubcategoryModel
        {
            SubcategoryId = reader.GetInt32("SubcategoryId"),
            CategoryId = reader.GetInt32("CategoryId"),
            SubcategoryName = reader.GetString("SubcategoryName")
        });

    public async Task<ServiceResponseModel> GetAllProductsAsync() =>
        await ExecuteQueryAsync<ProductModel>(_requests["GetAllProducts"], reader => new ProductModel
        {
            ProductId = reader.GetInt32("ProductId"),
            SubcategoryId = reader.GetInt32("SubcategoryId"),
            ProductName = reader.GetString("ProductName"),
            ProductDescription = reader.GetString("ProductDescription"),
            ProductImageUrl = reader.GetString("ProductImageUrl")
        });

    public async Task<ServiceResponseModel> GetAllImagesAsync() =>
        await ExecuteQueryAsync<ImageModel>(_requests["GetAllImages"], reader => new ImageModel
        {
            ImageId = reader.GetInt32("ImageId"),
            ProductId = reader.GetInt32("ProductId"),
            ImageUrl = reader.GetString("ImageUrl")
        });

    public async Task<ServiceResponseModel> GetAllDrawingsAsync() =>
        await ExecuteQueryAsync<DrawingModel>(_requests["GetAllDrawings"], reader => new DrawingModel
        {
            DrawingId = reader.GetInt32("DrawingId"),
            ProductId = reader.GetInt32("ProductId"),
            DrawingName = reader.GetString("DrawingName"),
            DrawingDescription = reader.GetString("DrawingDescription"),
            DrawingImageUrl = reader.GetString("DrawingImageUrl")
        });

    private async Task<ServiceResponseModel> ExecuteQueryAsync<T>(string query, Func<MySqlDataReader, T> mapFunction)
    {
        try
        {
            var resultList = new List<T>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        resultList.Add(mapFunction((MySqlDataReader)reader));
                    }
                }
            }

            return new ServiceResponseModel
            {
                Status = true,
                Message = "Data retrieved successfully.",
                Data = resultList
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
