using InformationService.Models.Response;

namespace InformationService.Interfaces
{
    public interface IProductsService
    {
        public Task<ServiceResponseModel> GetAllCategoriesAsync();
        public Task<ServiceResponseModel> GetAllSubcategoriesAsync();
        public Task<ServiceResponseModel> GetAllProductsAsync();
        public Task<ServiceResponseModel> GetAllImagesAsync();
        public Task<ServiceResponseModel> GetAllDrawingsAsync();
    }
}
