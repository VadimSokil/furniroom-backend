using FurniroomAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
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
        public async Task<ActionResult<string>> GetCategories()
        {
            var result = await _productsService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("subcategories-list")]
        public async Task<ActionResult<string>> GetSubcategories()
        {
            var result = await _productsService.GetAllSubcategoriesAsync();
            return Ok(result);
        }

        [HttpGet("products-list")]
        public async Task<ActionResult<string>> GetProducts()
        {
            var result = await _productsService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("images-list")]
        public async Task<ActionResult<string>> GetImages()
        {
            var result = await _productsService.GetAllImagesAsync();
            return Ok(result);
        }

        [HttpGet("drawings-list")]
        public async Task<ActionResult<string>> GetDrawingss()
        {
            var result = await _productsService.GetAllDrawingsAsync();
            return Ok(result);
        }
    }
}
