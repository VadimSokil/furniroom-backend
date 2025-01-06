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
            var note = await _productsService.GetAllCategoriesAsync();
            return Ok(note);
        }

        [HttpGet("subcategories-list")]
        public async Task<ActionResult<List<SubcategoryModel>>> GetAllSubcategories()
        {
            var note = await _productsService.GetAllSubcategoriesAsync();
            return Ok(note);
        }

        [HttpGet("products-list")]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
        {
            var note = await _productsService.GetAllProductsAsync();
            return Ok(note);
        }

        [HttpGet("images-list")]
        public async Task<ActionResult<List<ImageModel>>> GetAllImages()
        {
            var note = await _productsService.GetAllImagesAsync();
            return Ok(note);
        }

        [HttpGet("drawings-list")]
        public async Task<ActionResult<List<DrawingModel>>> GetAllDrawings()
        {
            var note = await _productsService.GetAllDrawingsAsync();
            return Ok(note);
        }
    }
}
