using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IProductSubcategoryService
    {
        public Task<List<ProductSubcategoryModel>> GetAllProductSubcategories();
    }
}
