using System;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Shipping
    {
        [Key]
        [Required]
        public int ShippingId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        public DateTime DeliveredAt { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Order? Order { get; set; }
    }
}
