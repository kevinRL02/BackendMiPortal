using PlatformService.Data;
using UserService1.Models;

namespace UserService1.Data
{
    public class UserRepoImp : IUserRepo
    {

        //Instancia del contexto de la DB
        private readonly AppDbContext _context;
        public UserRepoImp(AppDbContext context)
        {
            _context = context;
        }

        //Estos metodos referencian el metodo Users que esta en la clase AppDbContext 

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }
            return user;
        }


        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}