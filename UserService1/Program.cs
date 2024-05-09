using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using UserService1.Data;
using UserService1.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Esto se hace para la inyección de dependencias
IServiceCollection serviceCollection = builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMemo"));
//Para la inyección de dependencias de Usuarios
builder.Services.AddScoped<IUserRepo, UserRepoImp>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

//AddHttpClient<IShoppingCartDataClient, HttpShopingCartDataClient>(): Este método está configurando la inyección de dependencias para IShoppingCartDataClient y HttpShopingCartDataClient
builder.Services.AddHttpClient<IShoppingCartDataClient, HttpShopingCartDataClient>();


var app = builder.Build();
TempUserDataDb.LoadData(app);

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
