using System.Collections.Generic;
using System.Linq;
using ShoppingCartService.Models;

namespace ShoppingCartService.Data
{
    public class ShoppingCartImp : IShoppingCartRepo
    {
        private readonly AppDbContext _context;

        public ShoppingCartImp(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<ShoppingCart> GetAllShoppingCarts()
        {
            return _context.ShoppingCart.ToList() ?? new List<ShoppingCart>();
        }

        public ShoppingCart GetShoppingCartById(int id)
        {
            return _context.ShoppingCart.FirstOrDefault(c => c.Id == id) ?? throw new Exception($"Shopping cart with id {id} not found");
        }

        public void CreateShoppingCart(ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            _context.ShoppingCart.Add(cart);
        }

        public void UpdateShoppingCart(ShoppingCart cart)
        {

        }

        public void DeleteShoppingCart(ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            _context.ShoppingCart.Remove(cart);
        }

        public IEnumerable<ItemsShoppingCart> GetItemsByCartId(int cartId)
        {
            return _context.ItemsShoppingCart.Where(i => i.ShoppingCartId == cartId).ToList();
        }

        public ItemsShoppingCart GetItemById(int id)
        {
            return _context.ItemsShoppingCart.FirstOrDefault(i => i.Id == id);
        }

        public void AddItemToCart(ItemsShoppingCart item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.ItemsShoppingCart.Add(item);
        }

        public void UpdateItemInCart(ItemsShoppingCart item)
        {
            // Aquí no necesitas hacer nada, EF Core se encarga de esto.
        }

        public void RemoveItemFromCart(ItemsShoppingCart item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.ItemsShoppingCart.Remove(item);
        }

        public IEnumerable<ShoppingCart> GetShoppingCartsByUserId(int userId)
        {
            return _context.ShoppingCart.Where(c => c.UserId == userId).ToList() ?? new List<ShoppingCart>();
        }

        public IEnumerable<ItemsShoppingCart> GetItemsByShoppingCartId(int shoppingCartId)
        {
            return _context.ItemsShoppingCart.Where(i => i.ShoppingCartId == shoppingCartId).ToList();
        }
    }
}
