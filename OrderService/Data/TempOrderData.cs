using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using OrderService.Models;

namespace OrderService.Data
{
    public static class TempOrderData
    {
        public static void LoadData(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        public static void SeedData(AppDbContext context, bool isProd)
        {
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

            if (!context.Shippings.Any())
            {
                context.Shippings.AddRange(
                    new Shipping { Address = "123 Street", ClientName = "John Doe", City = "City", DeliveredAt = DateTime.Now, ClientId = 1 },
                    new Shipping {  Address = "456 Avenue", ClientName = "Jane Doe", City = "City", DeliveredAt = DateTime.Now, ClientId = 2 }
                );
                context.SaveChanges();
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order { CreatedAt = DateTime.Now, PaymentId = 1, ShippingId = 1, UserId = 1 },
                    new Order { CreatedAt = DateTime.Now, PaymentId = 2, ShippingId = 2, UserId = 2 }
                );
                context.SaveChanges();
            }
        }

    }
}
