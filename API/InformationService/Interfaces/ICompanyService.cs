using InformationService.Models.Company;

namespace InformationService.Interfaces
{
    public interface ICompanyService
    {
        public Task<List<CompanyModel>> GetCompanyInformationAsync();
        public Task<List<CompanyModel>> GetDeliveryInformationAsync();
        public Task<List<CompanyModel>> GetPaymentInformationAsync();
    }
}
