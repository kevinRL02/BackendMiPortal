using OrderService.Models;

namespace OrderService.EventModels
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public int ShoppingCartId {get;set;}

        public string Event { get; set; }

    }
}
