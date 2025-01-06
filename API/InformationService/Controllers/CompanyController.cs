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
        public async Task<ActionResult<List<CompanyModel>>> GetCompanyInformatiom()
        {
            var note = await _companyService.GetCompanyInformationAsync();
            return Ok(note);
        }

        [HttpGet("delivery-information")]
        public async Task<ActionResult<List<CompanyModel>>> GetDeliveryInformatiom()
        {
            var note = await _companyService.GetDeliveryInformationAsync();
            return Ok(note);
        }

        [HttpGet("payment-information")]
        public async Task<ActionResult<List<CompanyModel>>> GetPaymentInformatiom()
        {
            var note = await _companyService.GetPaymentInfromationAsync();
            return Ok(note);
        }
    }
}
