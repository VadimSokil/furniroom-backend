using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Request;
using FurniroomAPI.Models.Response;
using FurniroomAPI.Validation;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public ValidationMethods validationMethods = new ValidationMethods();

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<GatewayResponseModel>> AddOrder([FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(order.OrderId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderDate))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order date cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.AccountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(order.AccountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.PhoneNumber))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.PhoneNumber, 20))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Country))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Country cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Country, 100))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Country cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Region))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Region cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Region, 100))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Region cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.District))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "District cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.District, 100))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "District cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Street))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Street cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Street, 100))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Street cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.HouseNumber))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "House number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.HouseNumber, 20))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "House number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderText))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order text cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.OrderText, 5000))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order text cannot exceed 5000 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.DeliveryType))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Delivery type cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.DeliveryType, 20))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Delivery type cannot exceed 20 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _requestService.AddOrderAsync(order);
                var gatewayResponse = new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = serviceResponse.Status,
                    Message = serviceResponse.Message,
                    Data = serviceResponse.Data
                };
                return Ok(gatewayResponse);
            }
        }

        [HttpPost("add-question")]
        public async Task<ActionResult<GatewayResponseModel>> AddQuestion([FromBody] QuestionModel question)
        {
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(question.QuestionId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionDate))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question date cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.UserName))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "User name cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(question.UserName, 50))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "User name cannot exceed 50 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.PhoneNumber))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(question.PhoneNumber, 20))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionText))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question text cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(question.QuestionText, 5000))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question text cannot exceed 5000 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _requestService.AddQuestionAsync(question);
                var gatewayResponse = new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = serviceResponse.Status,
                    Message = serviceResponse.Message,
                    Data = serviceResponse.Data
                };
                return Ok(gatewayResponse);
            }
        }
    }
}
