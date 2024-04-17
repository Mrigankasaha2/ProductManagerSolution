using ProductBLL.Model;

namespace ProductBLL.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task CreateProductsAsync(Product product);
        Task UpdateProductsAsync(Product product);
        Task DeleteProductsAsync(int ProductID);
    }
}
