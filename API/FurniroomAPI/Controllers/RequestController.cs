using AccountsService.Models.Response;
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
                return CreateErrorResponse("Your query is missing some fields.");
            }

            var validationResult = ValidateOrder(order);
            if (validationResult != null)
            {
                return CreateErrorResponse(validationResult);
            }

            var serviceResponse = await _requestService.AddOrderAsync(order);
            return CreateGatewayResponse(serviceResponse);
        }

        private string ValidateOrder(OrderModel order)
        {
            if (!validationMethods.IsNotEmptyValue(order.OrderId))
                return "Order ID cannot be empty.";
            if (!validationMethods.IsValidDigit(order.OrderId))
                return "Order ID must be a positive number.";
            if (!validationMethods.IsNotEmptyValue(order.OrderDate))
                return "Order date cannot be empty.";
            if (!validationMethods.IsNotEmptyValue(order.AccountId))
                return "Account ID cannot be empty.";
            if (!validationMethods.IsValidDigit(order.AccountId))
                return "Account ID must be a positive number.";
            if (!validationMethods.IsNotEmptyValue(order.PhoneNumber))
                return "Phone number cannot be empty.";
            if (!validationMethods.IsValidLength(order.PhoneNumber, 20))
                return "Phone number cannot exceed 20 characters in length.";
            if (!validationMethods.IsNotEmptyValue(order.Country))
                return "Country cannot be empty.";
            if (!validationMethods.IsValidLength(order.Country, 100))
                return "Country cannot exceed 100 characters in length.";
            if (!validationMethods.IsNotEmptyValue(order.Region))
                return "Region cannot be empty.";
            if (!validationMethods.IsValidLength(order.Region, 100))
                return "Region cannot exceed 100 characters in length.";
            if (!validationMethods.IsNotEmptyValue(order.District))
                return "District cannot be empty.";
            if (!validationMethods.IsValidLength(order.District, 100))
                return "District cannot exceed 100 characters in length.";

            ValidateOptionalField(order.City, "City", 100);
            ValidateOptionalField(order.Village, "Village", 100);

            if (!validationMethods.IsNotEmptyValue(order.Street))
                return "Street cannot be empty.";
            if (!validationMethods.IsValidLength(order.Street, 100))
                return "Street cannot exceed 100 characters in length.";
            if (!validationMethods.IsNotEmptyValue(order.HouseNumber))
                return "House number cannot be empty.";
            if (!validationMethods.IsValidLength(order.HouseNumber, 20))
                return "House number cannot exceed 20 characters in length.";

            ValidateOptionalField(order.ApartmentNumber, "Apartment number", 20);

            if (!validationMethods.IsNotEmptyValue(order.OrderText))
                return "Order text cannot be empty.";
            if (!validationMethods.IsValidLength(order.OrderText, 5000))
                return "Order text cannot exceed 5000 characters in length.";
            if (!validationMethods.IsNotEmptyValue(order.DeliveryType))
                return "Delivery type cannot be empty.";
            if (!validationMethods.IsValidLength(order.DeliveryType, 20))
                return "Delivery type cannot exceed 20 characters in length.";

            return null; 
        }

        private void ValidateOptionalField(string? fieldValue, string fieldName, int maxLength)
        {
            if (validationMethods.IsNotEmptyValue(fieldValue) && !validationMethods.IsValidLength(fieldValue, maxLength))
            {
                throw new ArgumentException($"{fieldName} cannot exceed {maxLength} characters in length.");
            }
        }

        private GatewayResponseModel CreateErrorResponse(string message)
        {
            return new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = false,
                Message = message
            };
        }

        private ActionResult<GatewayResponseModel> CreateGatewayResponse(ServiceResponseModel serviceResponse)
        {
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
