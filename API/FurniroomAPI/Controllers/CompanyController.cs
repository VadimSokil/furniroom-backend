using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public string requestId = Guid.NewGuid().ToString();
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("company-information")]
        public async Task<ActionResult<string>> GetCompany(string requestId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: company-information");
            var serviceResponse = await _companyService.GetCompanyInformationAsync(requestId);
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };
            return Ok(gatewayResponse);
        }

        [HttpGet("delivery-information")]
        public async Task<ActionResult<string>> GetDelivery(string requestId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: delivery-information");
            var serviceResponse = await _companyService.GetDeliveryInformationAsync(requestId);
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };
            return Ok(gatewayResponse);
        }

        [HttpGet("payment-information")]
        public async Task<ActionResult<string>> GetPayment(string requestId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата:  {currentDateTime} , Получен новый запрос, Id запроса:  {requestId} , Тип: GET, Эндпоинт: payment-information");
            var serviceResponse = await _companyService.GetPaymentInformationAsync(requestId);
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
