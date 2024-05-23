using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProductsService.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductsService.Data
{
    public static class PrepData
    {
        public static void LoadData(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {


            if (context == null)
            {
                // Si appContex es nulo, no podemos continuar
                Console.WriteLine("Error: AppDbContext es nulo");
                return;
            }

            if (isProd)
            {
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }

            if (!context.ProductCategory.Any())
            {
                Console.WriteLine("Seeding data...");

                context.ProductCategory.AddRange(
                    new ProductCategory { Name = "Electronics" },
                    new ProductCategory { Name = "Books" },
                    new ProductCategory { Name = "Clothing" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}
