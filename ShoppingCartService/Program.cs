using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCartService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment; // Get the IWebHostEnvironment instance


var Configuration = builder.Configuration;// Add services to the container.


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IServiceCollection serviceCollection = builder.Services;


if (env.IsProduction())
{

    //SI LO DEJO ASI VA A ESTAR POR DEFECTO EN DEVELOPMENT ENVIORONMENT
    Console.WriteLine("Using SQL Server");
    serviceCollection.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("ShoppingCartCon")));
}

else if (env.IsDevelopment())
{
       Console.WriteLine("Using mem InMemo");
   //Esto se hace para la inyección de dependencias
   serviceCollection.AddDbContext<AppDbContext>(opt =>
       opt.UseInMemoryDatabase("InMemo"));
}


builder.Services.AddScoped<IShoppingCartRepo, ShoppingCartImp>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });;


builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>();

var app = builder.Build();


TempShoppingCartData.LoadData(app,env.IsProduction());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseRouting();
app.UseAuthorization();



app.UseEndpoints(endpints =>
{
    _ = endpints.MapControllers();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
