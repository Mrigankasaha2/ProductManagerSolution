using ProductBLL.Model;
using System.Text.Json;

namespace ProductBLL.Services
{
    public class ProductService : IProductService
    {
        private const string FilePath = "Data.json";
        public async Task CreateProductsAsync(Product product)
        {
            List<Product> products = await GetAllProductsAsync();
            if (products.Any(p => p.Brand == product.Brand && p.Name == product.Name))
            {
                throw new InvalidOperationException("A product with the same brand and name already exists.");
            }
            int ID = 1;
            if (products.Count != 0)
            {
                ID = products.Select(p => p.ID).Max() + 1;
            }
            product.ID = ID;
            products.Add(product);
            await WriteToJsonFileAsync(products);
        }

        public async Task DeleteProductsAsync(int ProductID)
        {
            List<Product> products = await GetAllProductsAsync();
            if (!products.Any(p => p.ID == ProductID))
            {
                throw new InvalidOperationException("Product not found.");
            }
            products.RemoveAll(p => p.ID == ProductID);
            await WriteToJsonFileAsync(products);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Product>();
            }

            using (FileStream fs = File.OpenRead(FilePath))
            {
                if (fs.Length == 0)
                {
                    return new List<Product>();
                }
                return await JsonSerializer.DeserializeAsync<List<Product>>(fs);
            }
        }

        public async Task UpdateProductsAsync(Product product)
        {
            List<Product> products = await GetAllProductsAsync();
            int index = products.FindIndex(p => p.ID == product.ID);
            if (index != -1)
            {
                if (products.Any(p => p.Brand == product.Brand && p.Name == product.Name))
                {
                    throw new InvalidOperationException("A product with the same brand and name already exists.");
                }
                products[index] = product;
                await WriteToJsonFileAsync(products);
            }
            else
            {
                throw new InvalidOperationException("Product not found.");
            }
        }

        private async Task WriteToJsonFileAsync(List<Product> products)
        {
            using FileStream fs = File.Create(FilePath);
            await JsonSerializer.SerializeAsync(fs, products);
        }
    }
}
