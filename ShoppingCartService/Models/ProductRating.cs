using System.ComponentModel.DataAnnotations;

namespace ShoppingCartService.Models
{
    public class ProductRating
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime RatingDate { get; set; }
        [Required]
        public int ProductId { get; set; } 
        [Required]
        public decimal Rating { get; set; }
        [Required]
        public int UserId { get; set; }
        public Products? Product{get;set;}
    }
}
