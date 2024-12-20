using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IDeliveryService
    {
        public Task<List<DeliveryPaymentsModel>> GetAllDeliveryInfo();
    }
}
