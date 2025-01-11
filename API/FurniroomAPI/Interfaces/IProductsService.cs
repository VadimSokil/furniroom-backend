namespace FurniroomAPI.Interfaces
{
    public interface IProductsService
    {
        public Task<string> GetAllCategoriesAsync();
        public Task<string> GetAllSubcategoriesAsync();
        public Task<string> GetAllProductsAsync();
        public Task<string> GetAllImagesAsync();
        public Task<string> GetAllDrawingsAsync();
    }
}
