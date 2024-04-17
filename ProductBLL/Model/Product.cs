using System.ComponentModel.DataAnnotations;

namespace ProductBLL.Model
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int Price { get; set; }
    }
}