using AccountsService.Models.Request;

namespace AccountsService.Interfaces
{
    public interface IRequestService
    {
        public Task AddOrderAsync(OrderModel order);
        public Task AddQuestionAsync(QuestionModel question);
    }
}
