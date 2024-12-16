using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IProductTypeService
    {
        public Task<List<ProductTypeModel>> GetAllProducts();
    }
}
