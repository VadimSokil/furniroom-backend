using InformationService.Models.Response;

namespace InformationService.Interfaces
{
    public interface IProductsService
    {
        public Task<ResponseModel> GetAllCategoriesAsync();
        public Task<ResponseModel> GetAllSubcategoriesAsync();
        public Task<ResponseModel> GetAllProductsAsync();
        public Task<ResponseModel> GetAllImagesAsync();
        public Task<ResponseModel> GetAllDrawingsAsync();
    }
}
