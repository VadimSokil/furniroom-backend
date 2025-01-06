using InformationService.Models.Products;

namespace InformationService.Interfaces
{
    public interface IProductsService
    {
        public Task<List<CategoryModel>> GetAllCategoriesAsync();
        public Task<List<SubcategoryModel>> GetAllSubcategoriesAsync();
        public Task<List<ProductModel>> GetAllProductsAsync();
        public Task<List<ImageModel>> GetAllImagesAsync();
        public Task<List<DrawingModel>> GetAllDrawingsAsync();
    }
}
