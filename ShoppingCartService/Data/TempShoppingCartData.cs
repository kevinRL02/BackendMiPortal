using ShoppingCartService.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartService.Data
{
    public static class TempShoppingCartData
    {

        //Para cargar datos a la DB
        public static void LoadData(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        //Este metodo se ññama en Program.cs, para inicializar la bd
        private static void SeedData(AppDbContext appContex, bool isProd)
        {
            if (appContex == null)
            {
                // Si appContex es nulo, no podemos continuar
                Console.WriteLine("Error: AppDbContext es nulo");
                return;
            }

            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    appContex.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!appContex.ShoppingCart.Any())
            {
                Console.WriteLine("Seeding data...");
                appContex.ShoppingCart.AddRange(
                    new ShoppingCart { UserId = 1 },
                    new ShoppingCart { UserId = 2 }
                );
                appContex.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data - not seeding");
            }

        }
    }
}