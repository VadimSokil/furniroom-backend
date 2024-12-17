using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IProductDrawingsService
    {
        public Task<List<ProductDrawingsModel>> GetAllProductDrawings();
    }
}
