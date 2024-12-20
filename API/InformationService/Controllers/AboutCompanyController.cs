using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutCompanyController : ControllerBase
    {
        private readonly IAboutCompanyService _aboutCompanyService;

        AboutCompanyController(IAboutCompanyService aboutCompanyService)
        {
            _aboutCompanyService = aboutCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AboutCompanyModel>>> GetAboutCompany()
        {
            var note = await _aboutCompanyService.GetAboutCompany();
            return Ok(note);
        }
    }
}
