using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Models.Response;
using AccountsService.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestsService;
        ValidationMethods validationMethods = new ValidationMethods();
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public RequestController(IRequestService requestsService)
        {
            _requestsService = requestsService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<ResponseModel>> AddOrder([FromBody] OrderModel order)
        {
            if (!validationMethods.IsNotEmptyValue(order.OrderId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order ID cannot be empty"
                };
            }
            else if (!validationMethods.IsValidDigit(order.OrderId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order ID must be a positive number"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderDate))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order date cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountID  cannot be empty"
                };
            }
            else if (!validationMethods.IsValidDigit(order.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.PhoneNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.PhoneNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number exceeds the maximum allowed length of 20 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Country))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Country cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.Country, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Country exceeds the maximum allowed length of 100 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Region))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Region cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.Region, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Region exceeds the maximum allowed length of 100 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.District))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "District cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.District, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "District exceeds the maximum allowed length of 100 characters"
                };
            }
            else if (!validationMethods.IsValidLength(order.City, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "City exceeds the maximum allowed length of 100 characters"
                };
            }
            else if (!validationMethods.IsValidLength(order.Village, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Village exceeds the maximum allowed length of 100 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Street))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Street cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.Street, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Street exceeds the maximum allowed length of 100 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.HouseNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "House number cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.HouseNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "House number exceeds the maximum allowed length of 20 characters"
                };
            }
            else if (!validationMethods.IsValidLength(order.ApartmentNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Apartment number exceeds the maximum allowed length of 20 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderText))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order text cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.OrderText, 5000))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order text exceeds the maximum allowed length of 5000 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.DeliveryType))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Delivery type cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(order.DeliveryType, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Delivery type exceeds the maximum allowed length of 20 characters"
                };
            }
            else
            {
                var result = await _requestsService.AddOrderAsync(order);
                return Ok(result);
            }
        }

        [HttpPost("add-question")]
        public async Task<ActionResult<ResponseModel>> AddQuestion([FromBody] QuestionModel question)
        {
            if (!validationMethods.IsNotEmptyValue(question.QuestionId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question ID cannot be empty"
                };
            }
            else if (!validationMethods.IsValidDigit(question.QuestionId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question ID must be a positive number"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionDate))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question date cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.UserName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "User name cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(question.UserName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "User name exceeds the maximum allowed length of 50 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.PhoneNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(question.PhoneNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number exceeds the maximum allowed length of 20 characters"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionText))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question text cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(question.QuestionText, 5000))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question text exceeds the maximum allowed length of 5000 characters"
                };
            }
            else
            {
                var result = await _requestsService.AddQuestionAsync(question);
                return Ok(result);
            }
        }
    }
}
