using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;

        public ProductsController(ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryModel>>> GetAllCategory()
        {
            var category = await _categoryService.GetAllCategories();
            return Ok(category);
        }

        [HttpGet("subcategories")]
        public async Task<ActionResult<List<SubcategoryModel>>> GetAllSubcategory()
        {
            var subcategory = await _subcategoryService.GetAllSubcategories();
            return Ok(subcategory);
        }

    }
}
