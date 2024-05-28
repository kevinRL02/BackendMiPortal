using System;
using System.Collections.Generic;
using System.Linq;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        // Orders
        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Add(order);
        }

        public Order GetOrderById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void UpdateOrder(Order order)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Remove(order);
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }


        // OrderItems
        public void CreateOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }
            _context.OrderItems.Add(orderItem);
        }

        public OrderItem GetOrderItemById(int id)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }
            return orderItem;
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.ToList();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteOrderItem(int id)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }
            _context.OrderItems.Remove(orderItem);
        }

        // Shippings
        public void CreateShipping(Shipping shipping)
        {
            if (shipping == null)
            {
                throw new ArgumentNullException(nameof(shipping));
            }
            _context.Shippings.Add(shipping);
        }

        public Shipping GetShippingById(int id)
        {
            var shipping = _context.Shippings.FirstOrDefault(s => s.ShippingId == id);
            if (shipping == null)
            {
                throw new ArgumentNullException(nameof(shipping));
            }
            return shipping;
        }

        public IEnumerable<Shipping> GetAllShippings()
        {
            return _context.Shippings.ToList();
        }

        public void UpdateShipping(Shipping shipping)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteShipping(int id)
        {
            var shipping = _context.Shippings.FirstOrDefault(s => s.ShippingId == id);
            if (shipping == null)
            {
                throw new ArgumentNullException(nameof(shipping));
            }
            _context.Shippings.Remove(shipping);
        }

        public IEnumerable<OrderItem> GetOrderItemsByUserId(int userId)
        {
            return _context.OrderItems
                .Where(oi => _context.Orders.Any(o => o.OrderId == oi.OrderId && o.UserId == userId))
                .ToList();
        }


        
    }
}
