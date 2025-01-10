using AccountsService.Models.Request;

namespace AccountsService.Interfaces
{
    public interface IRequestService
    {
        public Task<string> AddOrderAsync(OrderModel order);
        public Task<string> AddQuestionAsync(QuestionModel question);
    }
}
