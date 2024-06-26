using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Data;
using ShoppingCartService.Models;
using ShoppingCartService.SyncDataServices.Http;

using System.Collections.Generic;

namespace ShoppingCartService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class ItemsShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepo _repository;
        private readonly IUserServiceClient _userServiceCient;



        public ItemsShoppingCartController(IShoppingCartRepo repository,IUserServiceClient userServiceCient)
        {
            _repository = repository;
            _userServiceCient = userServiceCient;
        }

        [HttpGet("cart/{cartId}")]
        public ActionResult<IEnumerable<ItemsShoppingCart>> GetItemsByCartId(int cartId)
        {
            var items = _repository.GetItemsByCartId(cartId);
            return Ok(items);
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public ActionResult<ItemsShoppingCart> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost("cart/{cartId}")]
        public async Task<ActionResult<ItemsShoppingCart>> AddItemToCart(int cartId, ItemsShoppingCart item)
        {
            // Verificar que el carrito de compras existe
            var cart = _repository.GetShoppingCartById(cartId);
            if (cart == null)
            {
                return NotFound($"Shopping cart with ID {cartId} not found.");
            }

            // Verificar que el producto existe
            var product = await _userServiceCient.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                return NotFound($"Product with ID {item.ProductId} not found.");
            }

            // Asociar el ítem con el carrito
            item.ShoppingCartId = cartId;

            // Agregar el ítem al carrito
            _repository.AddItemToCart(item);
            _repository.SaveChanges();

            // Devolver una respuesta indicando que el ítem se ha agregado correctamente
            return CreatedAtRoute(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItemInCart(int id, ItemsShoppingCart item)
        {
            var itemFromRepo = _repository.GetItemById(id);
            if (itemFromRepo == null)
            {
                return NotFound();
            }

            itemFromRepo.QuantityProducts = item.QuantityProducts; // Actualiza los campos necesarios
            _repository.UpdateItemInCart(itemFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveItemFromCart(int id)
        {
            var itemFromRepo = _repository.GetItemById(id);
            if (itemFromRepo == null)
            {
                return NotFound();
            }

            _repository.RemoveItemFromCart(itemFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("cart/{cartId}")]
        public ActionResult ClearCart(int cartId)
        {
            var cart = _repository.GetShoppingCartById(cartId);
            if (cart == null)
            {
                return NotFound($"Shopping cart with ID {cartId} not found.");
            }

            var items = _repository.GetItemsByCartId(cartId);
            foreach (var item in items)
            {
                _repository.RemoveItemFromCart(item);
            }
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
