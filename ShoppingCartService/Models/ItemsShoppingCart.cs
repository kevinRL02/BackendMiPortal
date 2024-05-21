namespace ShoppingCartService.Models
{
    public class ItemsShoppingCart
    {
        public int Id{get;set;}
        public int IdShoppingCart {get;set;}
        public int IdProducts {get;set;}
        public int QuantityProducst {get;set;}
        public ShoppingCart ShoppingCart{get;set;}
        public Products Product { get; set; }
    }
}


