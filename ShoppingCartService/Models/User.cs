namespace ShoppingCartService.Models
{
     public class User
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}