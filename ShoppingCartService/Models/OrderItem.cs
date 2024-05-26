using System.ComponentModel.DataAnnotations;


namespace ShoppingCartService.Models
{
    public class OrderItem
    {
        [Key]
        [Required]
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Order? Order { get; set; }
    }
}
