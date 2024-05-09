using UserService1.Models;

namespace PlatformService.Data{
    public interface IUserRepo{
        bool saveChanges();
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id); 
        void AddUser(User user); 
    }
}