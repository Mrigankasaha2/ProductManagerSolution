using ProductBLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBLL.Services
{
    public class ProductService : IProductService
    {
        public async Task<List<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
