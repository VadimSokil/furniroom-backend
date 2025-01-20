using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("categories-list")]
        public async Task<ActionResult<GatewayResponseModel>> GetCategories()
        {
            var serviceResponse = await _productsService.GetAllCategoriesAsync();
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };

            return Ok(gatewayResponse);
        }

        [HttpGet("subcategories-list")]
        public async Task<ActionResult<GatewayResponseModel>> GetSubcategories()
        {
            var serviceResponse = await _productsService.GetAllSubcategoriesAsync();
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };
            return Ok(gatewayResponse);
        }

        [HttpGet("products-list")]
        public async Task<ActionResult<GatewayResponseModel>> GetProducts()
        {
            var serviceResponse = await _productsService.GetAllProductsAsync();
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };
            return Ok(gatewayResponse);
        }

        [HttpGet("images-list")]
        public async Task<ActionResult<GatewayResponseModel>> GetImages()
        {
            var serviceResponse = await _productsService.GetAllImagesAsync();
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };
            return Ok(gatewayResponse);
        }

        [HttpGet("drawings-list")]
        public async Task<ActionResult<GatewayResponseModel>> GetDrawingss()
        {
            var serviceResponse = await _productsService.GetAllDrawingsAsync();
            var gatewayResponse = new GatewayResponseModel
            {
                Date = currentDateTime,
                Status = serviceResponse.Status,
                Message = serviceResponse.Message,
                Data = serviceResponse.Data
            };
            return Ok(gatewayResponse);
        }
    }
}
