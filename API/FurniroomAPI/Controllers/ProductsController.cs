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

        public ProductsController(ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
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

        //[HttpGet("products")]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    //
        //}

        //[HttpGet("product-subcategories")]
        //public async Task<IActionResult> GetAllProductSubcategory()
        //{
        //    //
        //}

        //[HttpGet("product-gallery")]
        //public async Task<IActionResult> GetAllProductGallery()
        //{
        //    //
        //}

        //[HttpGet("product-drawings")]
        //public async Task<IActionResult> GetAllProductDrawings()
        //{
        //    //
        //}
    }
}
