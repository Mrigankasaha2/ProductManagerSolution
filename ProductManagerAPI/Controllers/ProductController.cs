using Microsoft.AspNetCore.Mvc;
using ProductBLL.Model;
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
        [Route ("getallproducts")]
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

        [HttpPost]
        [Route ("createnewproduct")]
        public async Task<IActionResult> CreateNewProduct([FromBody] Product product)
        {
            try
            {
                await _productService.CreateProductsAsync(product);
                return StatusCode(201, "Product created successfully");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating products");
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpPut]
        [Route ("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                await _productService.UpdateProductsAsync(product);
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating products");
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpDelete]
        [Route ("deleteproduct")]
        public async Task<IActionResult> DeleteProduct(int ProductID)
        {
            try
            {
                await _productService.DeleteProductsAsync(ProductID);
                return Ok("Product deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting products");
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }
    }
}
