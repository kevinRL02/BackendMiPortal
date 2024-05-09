
using UserService1.Models;
namespace UserService1.Data
{
    public static class TempUserDataDb
    {

        //Para cargar datos a la DB
        public static void LoadData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext? appContex)
{
    if (appContex == null)
    {
        // Si appContex es nulo, no podemos continuar
        Console.WriteLine("Error: AppDbContext es nulo");
        return;
    }

    if (!appContex.Users.Any())
    {
        Console.WriteLine("Guardando los datos");
        appContex.Users.AddRange(
            new User()
            {
                Name = "Pepo",
                Email = "pepo@gmail.com",
                Password = "123"
            },
            new User()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Password = "abc"
            },
            new User()
            {
                Name = "María",
                Email = "maria@gmail.com",
                Password = "xyz"
            }
            // Puedes agregar más usuarios según sea necesario
        );

        appContex.SaveChanges(); // Guardar los cambios en la base de datos
    }
}
    }
}