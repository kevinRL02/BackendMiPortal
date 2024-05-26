using OrderService.Models;
using System.Threading.Tasks;

namespace OrderService.SyncDataServices.Http
{
    public interface IShoppingCartClient 
    {
        Task<ShoppingCart> GetShoppingCartByUserId(int userId);
        Task<List<ItemsShoppingCart>> GetItemsByShoppingCartId(int cartId);
        Task ClearShoppingCart(int userId);
    }
}