using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IPaymentService
    {
        public Task<List<DeliveryPaymentsModel>> GetAllPaymentsInfo();
    }
}
