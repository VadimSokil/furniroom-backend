using AccountsService.Models.Response;
using AccountsService.Models.Request;

namespace AccountsService.Interfaces
{
    public interface IRequestService
    {
        public Task<ServiceResponseModel> AddOrderAsync(OrderModel order);
        public Task<ServiceResponseModel> AddQuestionAsync(QuestionModel question);
    }
}
