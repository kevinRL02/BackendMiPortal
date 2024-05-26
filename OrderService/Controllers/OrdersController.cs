using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;
using OrderService.SyncDataServices.Http;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IShoppingCartClient _shoppingCartClient;

        public OrderController(IOrderRepository repository, IShoppingCartClient shoppingCartClient)
        {
            _repository = repository;
            _shoppingCartClient = shoppingCartClient;
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = _repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var orders = _repository.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<Order>> CreateOrder(int userId, [FromBody] Shipping shipping)
        {
            var shoppingCart = await _shoppingCartClient.GetShoppingCartByUserId(userId);

            if (shoppingCart == null || shoppingCart.Id == 0)
            {
                return NotFound("Shopping cart not found");
            }

            Console.WriteLine($"Carrito de compras ID: {shoppingCart.Id}");
            Console.WriteLine($"Usuario ID: {shoppingCart.UserId}");

            var cartItems = await _shoppingCartClient.GetItemsByShoppingCartId(shoppingCart.Id);

            var orderItems = cartItems.ConvertAll(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.QuantityProducts
            });

            var payment = new Payment
            {
                CreatedAt = DateTime.UtcNow,
                Amount = CalculateTotalAmount(cartItems),
                Pending = false,
                Approved = true,
                UserId = userId
            };

            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                Payment = payment,
                Shipping = shipping,
                OrderItems = orderItems
            };

            _repository.CreateOrder(order);
            _repository.SaveChanges();

            await _shoppingCartClient.ClearShoppingCart(shoppingCart.Id);

            return CreatedAtRoute(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        private decimal CalculateTotalAmount(List<ItemsShoppingCart> cartItems)
        {
            // Replace with actual logic to calculate total amount
            return 100m;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            var existingOrder = _repository.GetOrderById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _repository.UpdateOrder(order);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            _repository.DeleteOrder(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
