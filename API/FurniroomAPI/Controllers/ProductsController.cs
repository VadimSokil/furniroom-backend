using FurniroomAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IProductService _productService;
        private readonly IProductSubcategoryService _productSubcategoryService;
        private readonly IProductGalleryService _productGalleryService;
        private readonly IProductDrawingsService _productDrawingsService;

        public ProductsController(ICategoryService categoryService, ISubcategoryService subcategoryService, IProductService productService, IProductSubcategoryService productSubcategoryService, IProductGalleryService productGalleryService, IProductDrawingsService productDrawingsService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _productService = productService;
            _productSubcategoryService = productSubcategoryService;
            _productGalleryService = productGalleryService;
            _productDrawingsService = productDrawingsService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategories();
            return Ok(subcategories);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("product-subcategories")]
        public async Task<IActionResult> GetAllProductSubcategory()
        {
            var productsubcategories = await _productSubcategoryService.GetAllProductSubcategory();
            return Ok(productsubcategories);
        }

        [HttpGet("product-gallery")]
        public async Task<IActionResult> GetAllProductGallery()
        {
            var gallery = await _productGalleryService.GetAllProductGallery();
            return Ok(gallery);
        }

        [HttpGet("product-drawings")]
        public async Task<IActionResult> GetAllProductDrawings()
        {
            var drawings = await _productDrawingsService.GetAllProductDrawings();
            return Ok(drawings);
        }
    }
}
