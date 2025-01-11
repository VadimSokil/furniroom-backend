namespace FurniroomAPI.Interfaces
{
    public interface ICompanyService
    {
        public Task<string> GetCompanyInformationAsync();
        public Task<string> GetDeliveryInformationAsync();
        public Task<string> GetPaymentInformationAsync();
    }
}
