using Microsoft.EntityFrameworkCore;
using ProductsService.Models; // Importa el espacio de nombres de las entidades

namespace ProductsService.Data
{
    //Este clase define una clase llamada AppDbContext que hereda de DbContext. DbContext es una clase proporcionada por Entity Framework Core que representa un contexto de base de datos y se utiliza para interactuar con la base de datos.
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductRating> ProductRating { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierProductsOrder> SupplierProductsOrder { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   

            // ProductCategory - Product
            // Una ProductCategory puede tener muchos Product.
            // Un Product pertenece a una ProductCategory.
            // La clave foránea es ProductCategoryId en Product.

            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId);

            //Product - ProductRating
            //Un Product puede tener muchos ProductRating.
            //Un ProductRating pertenece a un Product.
            //La clave foránea es ProductId en ProductRating.

            modelBuilder.Entity<Products>()
                .HasMany(p => p.ProductRatings)
                .WithOne(pr => pr.Products)
                .HasForeignKey(pr => pr.ProductId);

            //Product - SupplierProductsOrder
            //Un Product puede tener muchos SupplierProductsOrder.
            //Un SupplierProductsOrder pertenece a un Product.
            //La clave foránea es ProductId en SupplierProductsOrder.

            modelBuilder.Entity<Products>()
                .HasMany(p => p.SupplierProductsOrders)
                .WithOne(spo => spo.Products)
                .HasForeignKey(spo => spo.ProductId);

            //Supplier - SupplierProductsOrder
            //Un Supplier puede tener muchos SupplierProductsOrder.
            //Un SupplierProductsOrder pertenece a un Supplier.
            //La clave foránea es SupplierId en SupplierProductsOrder.

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.SupplierProductsOrders)
                .WithOne(spo => spo.Supplier)
                .HasForeignKey(spo => spo.SupplierId);
        }


    }
}
