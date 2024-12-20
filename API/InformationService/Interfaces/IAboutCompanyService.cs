using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IAboutCompanyService
    {
        public Task<List<AboutCompanyModel>> GetAboutCompany();
    }
}
