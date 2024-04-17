using ProductBLL.Model;

namespace ProductBLL.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync(int page = 1, int pageSize = 10, string searchTerm = "");

        Task CreateProductsAsync(Product product);

        Task UpdateProductsAsync(Product product);

        Task DeleteProductsAsync(int ProductID);
    }
}