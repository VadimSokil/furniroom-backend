using InformationService.Interfaces;
using InformationService.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("categories-list")]
        public async Task<ActionResult<List<CategoryModel>>> GetAllCategories()
        {
            var result = await _productsService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("subcategories-list")]
        public async Task<ActionResult<List<SubcategoryModel>>> GetAllSubcategories()
        {
            var result = await _productsService.GetAllSubcategoriesAsync();
            return Ok(result);
        }

        [HttpGet("products-list")]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
        {
            var result = await _productsService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("images-list")]
        public async Task<ActionResult<List<ImageModel>>> GetAllImages()
        {
            var result = await _productsService.GetAllImagesAsync();
            return Ok(result);
        }

        [HttpGet("modules-list")]
        public async Task<ActionResult<List<ModuleModel>>> GetAllModules()
        {
            var result = await _productsService.GetAllModulesAsync();
            return Ok(result);
        }
    }
}
