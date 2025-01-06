using RequestService.Models;

namespace RequestService.Interfaces
{
    public interface IRequestsService
    {
        public Task AddOrderAsync(OrderModel order);
        public Task AddQuestionAsync(QuestionModel question);
    }
}
