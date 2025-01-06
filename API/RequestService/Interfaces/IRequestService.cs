using RequestService.Models;

namespace RequestService.Interfaces
{
    public interface IRequestService
    {
        public Task AddOrderAsync(OrderModel order);
        public Task AddQuestionAsync(QuestionModel question);
    }
}
