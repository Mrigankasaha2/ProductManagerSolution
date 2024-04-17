using Microsoft.AspNetCore.Mvc;
using ProductBLL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var response = await _productService.GetAllProductsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products");
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }
    }
}
