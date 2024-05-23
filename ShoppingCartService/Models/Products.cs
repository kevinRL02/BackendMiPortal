using System.ComponentModel.DataAnnotations;

namespace ShoppingCartService.Models
{
    public class Products
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Inicializar con un valor por defecto

        public string Description { get; set; } = string.Empty;

        [Required]
        public int ProductCategoryId { get; set; } // CategoriaProductoID

        [Required]
        public decimal UnitCost { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int ReorderPoint { get; set; }

        public ICollection<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();

    }
}
