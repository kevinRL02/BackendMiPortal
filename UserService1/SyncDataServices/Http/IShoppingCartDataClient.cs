using UserService1.Dtos;

namespace UserService1.SyncDataServices.Http{
    public interface IShoppingCartDataClient
    {
        public Task SendUserToShoppingCart(UserReadDto userReadDto); 
    }
}