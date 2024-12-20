namespace FurniroomAPI.Interfaces
{
    public interface IPaymentService
    {
        public Task<string> GetAllPaymentInfo();
    }
}
