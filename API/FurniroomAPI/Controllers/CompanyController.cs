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
        public async Task<ActionResult<string>> GetCompany()
        {
            Console.WriteLine($"[{currentDateTime}]: Получен новый запрос, айди: {requestId} , тип: GET , эндпоинт: company-information");
            var serviceResponse = await _companyService.GetCompanyInformationAsync();
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
        public async Task<ActionResult<string>> GetDelivery()
        {
            Console.WriteLine($"[{currentDateTime}]: Получен новый запрос, айди: {requestId}, тип: GET, эндпоинт: delivery-information");
            var serviceResponse = await _companyService.GetDeliveryInformationAsync();
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
        public async Task<ActionResult<string>> GetPayment()
        {
            Console.WriteLine($"[{currentDateTime}]: Получен новый запрос, айди: {requestId}, тип: GET, эндпоинт: payment-information");
            var serviceResponse = await _companyService.GetPaymentInformationAsync();
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
