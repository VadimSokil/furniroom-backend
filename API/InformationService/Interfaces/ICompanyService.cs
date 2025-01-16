using InformationService.Models.Response;

namespace InformationService.Interfaces
{
    public interface ICompanyService
    {
        public Task<ResponseModel> GetCompanyInformationAsync();
        public Task<ResponseModel> GetDeliveryInformationAsync();
        public Task<ResponseModel> GetPaymentInformationAsync();
    }
}
