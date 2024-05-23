using ShoppingCartService.Models;
using System.Threading.Tasks;

namespace ShoppingCartService.SyncDataServices.Http
{
    public interface IUserServiceClient
    {
        Task<User> GetUserByIdAsync(int id);
    }
}