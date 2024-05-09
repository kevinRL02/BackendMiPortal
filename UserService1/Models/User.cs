using System.ComponentModel.DataAnnotations;

namespace UserService1.Models
{
    public class User{
        [Key]
        [Required]
        public int Id{get;set;}
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email {get;set;}
    }
}