using System.Collections.Generic;
using ShoppingCartService.Models; // Asegúrate de que esto esté correcto

namespace ShoppingCartService.Data{
    public interface IShoppingCartRepo{
        bool SaveChanges();

         // Métodos para manejar los carritos de compras
        IEnumerable<ShoppingCart> GetAllShoppingCarts();
        ShoppingCart GetShoppingCartById(int id);
        void CreateShoppingCart(ShoppingCart cart);
        void UpdateShoppingCart(ShoppingCart cart);
        void DeleteShoppingCart(ShoppingCart cart);

        IEnumerable<ShoppingCart> GetShoppingCartsByUserId(int userId);

        // Métodos para manejar los ítems del carrito de compras
        IEnumerable<ItemsShoppingCart> GetItemsByCartId(int cartId);
        ItemsShoppingCart GetItemById(int id);
        void AddItemToCart(ItemsShoppingCart item);
        void UpdateItemInCart(ItemsShoppingCart item);
        void RemoveItemFromCart(ItemsShoppingCart item);

        IEnumerable<ItemsShoppingCart> GetItemsByShoppingCartId(int shoppingCartId);

    }
}