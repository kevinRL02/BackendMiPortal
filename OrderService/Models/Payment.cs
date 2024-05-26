using System;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool Pending { get; set; }

        [Required]
        public bool Approved { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; } 

        public Order? Order { get; set; }
    }
}
