using InformationService.Models.Response;

namespace InformationService.Interfaces
{
    public interface ICompanyService
    {
        public Task<ServiceResponseModel> GetCompanyInformationAsync();
        public Task<ServiceResponseModel> GetDeliveryInformationAsync();
        public Task<ServiceResponseModel> GetPaymentInformationAsync();
    }
}
