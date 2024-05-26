using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class User{
        [Key]
        [Required]
        public int Id{get;set;}
        [Required]
        public required string UserName { get; set; }

    }
}