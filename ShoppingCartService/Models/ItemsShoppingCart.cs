using System.ComponentModel.DataAnnotations;

namespace ShoppingCartService.Models
{
    public class ItemsShoppingCart
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ShoppingCartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int QuantityProducts { get; set; }

        public ShoppingCart? ShoppingCart { get; set; } // Permitir nulo

        public Products? Product { get; set; } // Permitir nulo
    }
}
