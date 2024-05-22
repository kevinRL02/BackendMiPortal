using System.ComponentModel.DataAnnotations;

namespace ShoppingCartService.Models
{
     public class User
    {   
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string ExternalId { get; set; } = null!; // Inicializar como 
        [Required]
        public string UserName { get; set; } =string.Empty;
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}