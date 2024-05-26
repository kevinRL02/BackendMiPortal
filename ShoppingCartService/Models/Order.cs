using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartService.Models 

{
    public class Order
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PaymentId { get; set; }

        [Required]
        public int ShippingId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        public User? User { get; set; } 
    }
}
