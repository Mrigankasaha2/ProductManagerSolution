using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductBLL.Model;
using ProductBLL.Services;
using ProductManagerAPI.Controllers;

namespace ProductTest
{
    public class ProductUnitTest
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly ProductController _controller;

        public ProductUnitTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_loggerMock.Object, _productServiceMock.Object);
        }

        [Fact]
        public async Task GetAllProduct_Returns_Ok()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _controller.GetAllProduct();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateNewProduct_With_Valid_Product_Returns_Created()
        {
            // Arrange
            var product = new Product { Name = "TestName", Brand = "TestBrand", Price = 10 };
            _productServiceMock.Setup(x => x.CreateProductsAsync(It.IsAny<Product>())).Verifiable();

            // Act
            var result = await _controller.CreateNewProduct(product);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, ((ObjectResult)result).StatusCode);

            // Verify that the service method was called
            _productServiceMock.Verify(x => x.CreateProductsAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_With_Valid_Product_Returns_Ok()
        {
            // Arrange
            var product = new Product { ID = 1, Name = "TestName", Brand = "TestBrand", Price = 10 };

            // Act
            var result = await _controller.UpdateProduct(product);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_With_Valid_ProductID_Returns_Ok()
        {
            // Arrange
            int productId = 1;

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}