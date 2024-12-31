using FurniroomAPI.Models;

namespace FurniroomAPI.Interfaces
{
    public interface IOrderService
    {
        public Task AddOrder(OrderModel order);
    }
}
