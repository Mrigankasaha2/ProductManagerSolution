using ProductBLL.Model;

namespace ProductBLL.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
