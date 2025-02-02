using AccountsService.Models.Response;

namespace FurniroomAPI.Interfaces
{
    public interface ICompanyService
    {
        public Task<ServiceResponseModel> GetCompanyInformationAsync(string requestId);
        public Task<ServiceResponseModel> GetDeliveryInformationAsync(string requestId);
        public Task<ServiceResponseModel> GetPaymentInformationAsync(string requestId);
    }
}
