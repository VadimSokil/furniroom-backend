using AccountsService.Models.Response;

namespace FurniroomAPI.Interfaces
{
    public interface IProductsService
    {
        public Task<ServiceResponseModel> GetAllCategoriesAsync(string requestId);
        public Task<ServiceResponseModel> GetAllSubcategoriesAsync(string requestId);
        public Task<ServiceResponseModel> GetAllProductsAsync(string requestId);
        public Task<ServiceResponseModel> GetAllImagesAsync(string requestId);
        public Task<ServiceResponseModel> GetAllModulesAsync(string requestId);
        public Task<ServiceResponseModel> GetAllSizesAsync(string requestId);
        public Task<ServiceResponseModel> GetAllColorsAsync(string requestId);
    }
}
