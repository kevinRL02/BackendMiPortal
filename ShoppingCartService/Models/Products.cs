namespace ShoppingCartService.Models
{
    public class ItemsShoppingCart
    {
        public int Id{get;set;}
        public int Name {get;set}
        public string Description {get;set}
        public int UnitCost {get;set}
        public ShoppingCart ShoppingCart{get;set}
    }
}