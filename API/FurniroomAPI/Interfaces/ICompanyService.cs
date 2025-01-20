using AccountsService.Models.Response;

namespace FurniroomAPI.Interfaces
{
    public interface ICompanyService
    {
        public Task<ServiceResponseModel> GetCompanyInformationAsync();
        public Task<ServiceResponseModel> GetDeliveryInformationAsync();
        public Task<ServiceResponseModel> GetPaymentInformationAsync();
    }
}
