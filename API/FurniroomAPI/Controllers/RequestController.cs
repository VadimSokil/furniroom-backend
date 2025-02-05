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
        public string requestId = Guid.NewGuid().ToString();
        public ValidationMethods validationMethods = new ValidationMethods();

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<GatewayResponseModel>> AddOrder([FromBody] OrderModel order)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: add-order");

            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Ваш запрос не содержит всех необходимых полей.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Order ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(order.OrderId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Order ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderDate))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Дата заказа не может быть пустой.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order date cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.AccountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(order.AccountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.PhoneNumber))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер телефона не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.PhoneNumber, 20))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер телефона не может превышать 20 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Country))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Страна не может быть пустой.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Country cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Country, 100))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Страна не может превышать 100 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Country cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Region))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Регион не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Region cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Region, 100))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Регион не может превышать 100 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Region cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.District))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Район не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "District cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.District, 100))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Район не может превышать 100 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "District cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.City))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Город не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "City cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.City, 100))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Город не может превышать 100 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "City cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Village))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Деревня не может быть пустой.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Village cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Village, 100))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Деревня не может превышать 100 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Village cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.Street))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Улица не может быть пустой.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Street cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.Street, 100))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Улица не может превышать 100 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Street cannot exceed 100 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.HouseNumber))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер дома не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "House number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.HouseNumber, 20))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер дома не может превышать 20 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "House number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.ApartmentNumber))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер квартиры не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Apartment number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.ApartmentNumber, 20))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер квартиры не может превышать 20 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Apartment number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.OrderText))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Текст заказа не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order text cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.OrderText, 5000))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Текст заказа не может превышать 5000 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Order text cannot exceed 5000 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(order.DeliveryType))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Тип доставки не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Delivery type cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(order.DeliveryType, 20))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Тип доставки не может превышать 20 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Delivery type cannot exceed 20 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _requestService.AddOrderAsync(order, requestId);
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
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: add-question");

            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Ваш запрос не содержит всех необходимых полей.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Question ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(question.QuestionId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Question ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionDate))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Дата вопроса не может быть пустой.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question date cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.UserName))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Имя пользователя не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "User name cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(question.UserName, 50))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Имя пользователя не может превышать 50 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "User name cannot exceed 50 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.PhoneNumber))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер телефона не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(question.PhoneNumber, 20))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Номер телефона не может превышать 20 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Phone number cannot exceed 20 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(question.QuestionText))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Текст вопроса не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question text cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(question.QuestionText, 5000))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Текст вопроса не может превышать 5000 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Question text cannot exceed 5000 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _requestService.AddQuestionAsync(question, requestId);
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
