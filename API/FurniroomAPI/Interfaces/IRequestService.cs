using FurniroomAPI.Models.Request;

namespace FurniroomAPI.Interfaces
{
    public interface IRequestService
    {
        public Task<string> AddOrderAsync(OrderModel order);
        public Task<string> AddQuestionAsync(QuestionModel question);
    }
}
