using AccountsService.Models.Response;
using AccountsService.Models.Request;

namespace AccountsService.Interfaces
{
    public interface IRequestService
    {
        public Task<ResponseModel> AddOrderAsync(OrderModel order);
        public Task<ResponseModel> AddQuestionAsync(QuestionModel question);
    }
}
