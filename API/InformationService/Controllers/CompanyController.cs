using InformationService.Interfaces;
using InformationService.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers
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
        public async Task<ActionResult<List<CompanyModel>>> GetCompanyInformation()
        {
            var result = await _companyService.GetCompanyInformationAsync();
            return Ok(result);
        }

        [HttpGet("delivery-information")]
        public async Task<ActionResult<List<CompanyModel>>> GetDeliveryInformation()
        {
            var result = await _companyService.GetDeliveryInformationAsync();
            return Ok(result);
        }

        [HttpGet("payment-information")]
        public async Task<ActionResult<List<CompanyModel>>> GetPaymentInformation()
        {
            var result = await _companyService.GetPaymentInformationAsync();
            return Ok(result);
        }

    }
}
