using AccountsService.Models.Response;
using FurniroomAPI.Models.Request;

namespace FurniroomAPI.Interfaces
{
    public interface IRequestService
    {
        public Task<ServiceResponseModel> AddOrderAsync(OrderModel order);
        public Task<ServiceResponseModel> AddQuestionAsync(QuestionModel question);
    }
}
