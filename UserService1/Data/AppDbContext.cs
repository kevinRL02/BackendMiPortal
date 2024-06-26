using Microsoft.EntityFrameworkCore;
using UserService1.Models; // Importa el espacio de nombres de las entidades

namespace UserService1.Data
{
    //Este clase define una clase llamada AppDbContext que hereda de DbContext. DbContext es una clase proporcionada por Entity Framework Core que representa un contexto de base de datos y se utiliza para interactuar con la base de datos.
    public class AppDbContext : DbContext
    {
        //. Toma un parámetro de tipo DbContextOptions<AppDbContext>, que proporciona las opciones de configuración para el contexto de la base de datos. 
        //Llama al constructor base de la clase DbContext y pasa las opciones proporcionadas.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // Agrega la declaración de DbSet para la entidad User
        //public DbSet<User> Users { get; set; }: Este es un DbSet que representa una colección de entidades de usuario en la base de datos. DbSet es una propiedad de DbContext que permite acceder y realizar operaciones CRUD en una tabla de la base de datos. 
        //En este caso, la entidad de usuario está representada por la clase User definida en el espacio de nombres UserService1.Models.
        public DbSet<User> Users { get; set; }
    }
}
