using AccountsService.Models.Response;

namespace FurniroomAPI.Interfaces
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
