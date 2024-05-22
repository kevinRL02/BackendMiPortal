using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Data;
using ShoppingCartService.Models;
using System.Collections.Generic;

namespace ShoppingCartService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartRepo _repository;

        public ShoppingCartsController(IShoppingCartRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShoppingCart>> GetAllShoppingCarts()
        {
            var shoppingCarts = _repository.GetAllShoppingCarts();
            return Ok(shoppingCarts);
        }

        [HttpGet("{id}", Name = "GetShoppingCartById")]
        public ActionResult<ShoppingCart> GetShoppingCartById(int id)
        {
            var shoppingCart = _repository.GetShoppingCartById(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return Ok(shoppingCart);
        }

        [HttpPost("user/{userId}")]
        public ActionResult<ShoppingCart> CreateShoppingCart(int userId, ShoppingCart shoppingCart)
        {
            // Asociar el ID del usuario al carrito de compra
            shoppingCart.UserId = userId;

            _repository.CreateShoppingCart(shoppingCart);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetShoppingCartById), new { id = shoppingCart.Id }, shoppingCart);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateShoppingCart(int id, ShoppingCart shoppingCart)
        {
            var shoppingCartFromRepo = _repository.GetShoppingCartById(id);
            if (shoppingCartFromRepo == null)
            {
                return NotFound();
            }

            shoppingCartFromRepo.UserId = shoppingCart.UserId; // Actualiza los campos necesarios
            shoppingCartFromRepo.Items = shoppingCart.Items;
            _repository.UpdateShoppingCart(shoppingCartFromRepo);
            
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteShoppingCart(int id)
        {
            var shoppingCartFromRepo = _repository.GetShoppingCartById(id);
            if (shoppingCartFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteShoppingCart(shoppingCartFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<ShoppingCart>> GetShoppingCartsByUserId(int userId)
        {
            var shoppingCarts = _repository.GetShoppingCartsByUserId(userId);
            return Ok(shoppingCarts);
        }

        [HttpPost("user/{userId}/add")]
        public ActionResult<ItemsShoppingCart> AddItemToShoppingCart(int userId, [FromBody] ItemsShoppingCart item)
        {
            var cart = _repository.GetShoppingCartsByUserId(userId).FirstOrDefault();
            if (cart == null)
            {
                return NotFound($"Shopping cart for user {userId} not found.");
            }

            item.ShoppingCartId = cart.Id; // Asegurarse de que el ítem se asocie al carrito correcto
            _repository.AddItemToCart(item);
            _repository.SaveChanges();
            return Ok("item");
        }
    }
}
