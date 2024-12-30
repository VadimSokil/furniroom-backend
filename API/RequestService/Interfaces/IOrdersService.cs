using RequestService.Models;

namespace RequestService.Interfaces
{
    public interface IOrdersService
    {
        public Task AddOrder(OrderModel order);
    }
}
