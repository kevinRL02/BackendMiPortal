using Microsoft.EntityFrameworkCore;
using OrderService.Data; // Importa el espacio de nombres de las entidades
using OrderService.Models; // Importa el espacio de nombres de las entidades

namespace OrderService.Data
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductRating> ProductRating { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierProductsOrder> SupplierProductsOrder { get; set; }



        /// 

        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ItemsShoppingCart> ItemsShoppingCart { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<User>();
            modelBuilder.Ignore<ItemsShoppingCart>();
            modelBuilder.Ignore<ShoppingCart>();
            modelBuilder.Ignore<Payment>();
            modelBuilder.Ignore<ProductCategory>();
            modelBuilder.Ignore<ProductRating>();
            modelBuilder.Ignore<Products>();
            modelBuilder.Ignore<Supplier>();
            modelBuilder.Ignore<SupplierProductsOrder>();

            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shipping)
                .WithOne(s => s.Order)
                .HasForeignKey<Order>(o => o.ShippingId);

        }


    }
}
