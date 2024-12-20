using FurniroomAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutCompanyController : ControllerBase
    {
        private readonly IAboutCompanyService _aboutCompanyService;

        public AboutCompanyController(IAboutCompanyService aboutCompanyService)
        {
            _aboutCompanyService = aboutCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutCompany()
        {
            var notes = await _aboutCompanyService.GetAboutCompany();
            return Ok(notes);
        }
    }
}
