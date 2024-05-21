using System.ComponentModel.DataAnnotations;

namespace UserService1.Models
{
    public class User{
        [Key]
        [Required]
        public int Id{get;set;}
        [Required]
        public required string UserName { get; set; }

    }
}