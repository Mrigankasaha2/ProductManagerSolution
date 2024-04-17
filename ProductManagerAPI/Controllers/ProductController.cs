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
        [Route("getallproducts")]
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
        [Route("createnewproduct")]
        public async Task<IActionResult> CreateNewProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogWarning("Received a null product request.");
                    return BadRequest("Request is null or empty");
                }
                await _productService.CreateProductsAsync(product);
                _logger.LogInformation("Product created successfully: {ProductName}", product.Name);
                return StatusCode(201, "Product created successfully");
            }
            catch(InvalidOperationException exception)
            {
                _logger.LogError(exception, "An error occurred while creating products");
                return StatusCode(500, exception.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating products");
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpPut]
        [Route("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogWarning("Received a null product request.");
                    return BadRequest("Request is null or empty");
                }
                if (product.ID == 0)
                {
                    _logger.LogWarning("Received a request with productid 0.");
                    return BadRequest("No product with ID 0");
                }
                await _productService.UpdateProductsAsync(product);
                _logger.LogInformation("Product updated successfully: {ProductName}", product.Name);
                return Ok("Product updated successfully");
            }
            catch (InvalidOperationException exception)
            {
                _logger.LogError(exception, "An error occurred while updating products");
                return StatusCode(500, exception.Message);
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
                if (ProductID == 0)
                {
                    _logger.LogWarning("Received a request with productid 0.");
                    return BadRequest("No product with ID 0");
                }
                await _productService.DeleteProductsAsync(ProductID);
                _logger.LogInformation("Product deleted successfully: {ProductID}", ProductID);
                return Ok("Product deleted successfully");
            }

            catch (InvalidOperationException exception)
            {
                _logger.LogError(exception, "An error occurred while deleting products");
                return StatusCode(500, exception.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting products");
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }
    }
}
