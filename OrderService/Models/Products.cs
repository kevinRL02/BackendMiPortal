using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Products
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty; // Inicializar con un valor por defecto
        [Required]
        public int ProductCategoryId { get; set; } // CategoriaProductoID
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal UnitCost { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int ReorderPoint { get; set; }
        public ICollection<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();
        public ProductCategory? ProductCategory { get; set; } 
        public ICollection<SupplierProductsOrder> SupplierProductsOrders { get; set; } = new List<SupplierProductsOrder>();

    }
}
