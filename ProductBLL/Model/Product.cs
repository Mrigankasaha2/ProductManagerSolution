using System.ComponentModel.DataAnnotations;

namespace ProductBLL.Model
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
    }
}