using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryModel>> GetAllCategories();

    }
}
