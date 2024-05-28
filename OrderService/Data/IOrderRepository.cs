using OrderService.Data;
using OrderService.Models;


namespace OrderService.Data
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Order GetOrderById(int id);
        IEnumerable<Order> GetAllOrders();
        void UpdateOrder(Order order);
        void DeleteOrder(int id);

        IEnumerable<Order> GetOrdersByUserId(int userId);  // Nuevo método


        ///

        void CreateOrderItem(OrderItem orderItem);
        OrderItem GetOrderItemById(int id);
        IEnumerable<OrderItem> GetAllOrderItems();
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(int id);

        //
        void CreateShipping(Shipping shipping);
        Shipping GetShippingById(int id);
        IEnumerable<Shipping> GetAllShippings();
        void UpdateShipping(Shipping shipping);
        void DeleteShipping(int id);
        bool SaveChanges();


        IEnumerable<OrderItem> GetOrderItemsByUserId(int userId);



    }


}

