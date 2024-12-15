using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface ISubcategoryService
    {
        public Task<List<SubcategoryModel>> GetAllSubcategories();
    }
}
