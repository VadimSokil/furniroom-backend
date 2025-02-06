using AccountsService.Models.Response;
using static FurniroomAPI.Models.RequestModels;

namespace FurniroomAPI.Interfaces
{
    public interface IRequestService
    {
        public Task<ServiceResponseModel> AddOrderAsync(OrderModel order, string requestId);
        public Task<ServiceResponseModel> AddQuestionAsync(QuestionModel question, string requestId);
    }
}
