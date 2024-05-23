using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Data;
using ShoppingCartService.Models;
using ShoppingCartService.SyncDataServices.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartRepo _repository;
        private readonly IUserServiceClient _userServiceClient;

        public ShoppingCartsController(IShoppingCartRepo repository, IUserServiceClient userServiceClient)
        {
            _repository = repository;
            _userServiceClient = userServiceClient;
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
        public async Task<ActionResult<ShoppingCart>> CreateShoppingCart(int userId, ShoppingCart shoppingCart)
        {
            var user = await _userServiceClient.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

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
            return Ok(item);
        }
    }
}
