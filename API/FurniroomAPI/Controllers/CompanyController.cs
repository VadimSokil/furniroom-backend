using FurniroomAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("company-information")]
        public async Task<ActionResult<string>> GetCompany()
        {
            var result = await _companyService.GetCompanyInformationAsync();
            return Ok(result);
        }

        [HttpGet("delivery-information")]
        public async Task<ActionResult<string>> GetDelivery()
        {
            var result = await _companyService.GetDeliveryInformationAsync();
            return Ok(result);
        }

        [HttpGet("payment-information")]
        public async Task<ActionResult<string>> GetPayment()
        {
            var result = await _companyService.GetPaymentInformationAsync();
            return Ok(result);
        }
    }
}
