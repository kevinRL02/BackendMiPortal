using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
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
        public Payment? Payment { get; set; }
        public Shipping? Shipping { get; set; }
    }
}
