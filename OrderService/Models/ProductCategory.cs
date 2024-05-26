using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class ProductCategory
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Products> Products { get; set; } = new List<Products>();

    }
}
