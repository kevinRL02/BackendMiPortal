using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using UserService1.Data;
using UserService1.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment; // Get the IWebHostEnvironment instance



var Configuration = builder.Configuration;// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowSpecificOrigins, 
        builder =>
    {
        builder.WithOrigins("http://localhost",
            "http://localhost:4200",
            "https://localhost:7230",
            "http://localhost:90"
            )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithHeaders("ngrok-skip-browser-warning", "Content-Type", "Authorization") // Explicitly add custom headers here
        .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

IServiceCollection serviceCollection = builder.Services;

// Use the env instance to check the environment

if (env.IsProduction())
{

    //SI LO DEJO ASI VA A ESTAR POR DEFECTO EN DEVELOPMENT ENVIORONMENT
    Console.WriteLine("Using SQL Server");
    serviceCollection.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
}


else if (env.IsDevelopment())
{
       Console.WriteLine("Using mem InMemo");
   //Esto se hace para la inyección de dependencias
   serviceCollection.AddDbContext<AppDbContext>(opt =>
       opt.UseInMemoryDatabase("InMemo"));
}

//Para la inyección de dependencias de Usuarios
builder.Services.AddScoped<IUserRepo, UserRepoImp>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

//AddHttpClient<IShoppingCartDataClient, HttpShopingCartDataClient>(): Este método está configurando la inyección de dependencias para IShoppingCartDataClient y HttpShopingCartDataClient
builder.Services.AddHttpClient<IShoppingCartDataClient, HttpShopingCartDataClient>();


var app = builder.Build();

//Para que se carguen los datos quemados
TempUserDataDb.LoadData(app,env.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
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

app.UseCors(MyAllowSpecificOrigins);


app.UseEndpoints(endpints =>
{
    _ = endpints.MapControllers();
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
