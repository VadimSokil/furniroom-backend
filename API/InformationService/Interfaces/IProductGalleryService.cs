using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IProductGalleryService
    {
        public Task<List<ProductGalleryModel>> GetAllProductGallery();
    }
}
