namespace ShoppingCartService.Models
{
     public class Products
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public int Stock { get; set; }
    }
}
