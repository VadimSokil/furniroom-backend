using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICategoryService _apiService;

        public ProductsController(ICategoryService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryModel>>> GetAllCategory()
        {
            var notes = await _apiService.GetAllCategories();
            return Ok(notes);
        }

    }
}
