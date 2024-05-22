using System.ComponentModel.DataAnnotations;

namespace ShoppingCartService.Models
{
     public class ShoppingCart
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; } 
        public ICollection<ItemsShoppingCart> Items { get; set; } = new List<ItemsShoppingCart>();
    }
}
