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
        private readonly IProductTypeService _productTypeService;
        private readonly IProductSubcategoryService _productSubcategoryService;
        private readonly IProductGalleryService _productGalleryService;
        private readonly IProductDrawingsService _productDrawingsService;

        public ProductsController(ICategoryService categoryService, ISubcategoryService subcategoryService, IProductTypeService productTypeService, IProductSubcategoryService productSubcategoryService, IProductGalleryService productGalleryService, IProductDrawingsService productDrawingsService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _productTypeService = productTypeService;
            _productSubcategoryService = productSubcategoryService;
            _productGalleryService = productGalleryService;
            _productDrawingsService = productDrawingsService;
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

        [HttpGet("products")]
        public async Task<ActionResult<List<ProductTypeModel>>> GetAllProducts()
        {
            var products = await _productTypeService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("product-subcategories")]
        public async Task<ActionResult<List<ProductSubcategoryModel>>> GetAllProductSubcategory()
        {
            var productsubcategory = await _subcategoryService.GetAllSubcategories();
            return Ok(productsubcategory);
        }

        [HttpGet("product-gallery")]
        public async Task<ActionResult<List<ProductGalleryModel>>> GetAllProductGallery()
        {
            var productgallery = await _productGalleryService.GetAllProductGallery();
            return Ok(productgallery);
        }

        [HttpGet("product-drawings")]
        public async Task<ActionResult<List<ProductDrawingsModel>>> GetAllProductDrawings()
        {
            var productdrawings = await _productDrawingsService.GetAllProductDrawings();
            return Ok(productdrawings);
        }
    }
}
