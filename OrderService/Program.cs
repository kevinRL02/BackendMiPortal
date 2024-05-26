using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.SyncDataServices.Http;
using OrderService.AsyncDataServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



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
        opt.UseSqlServer(Configuration.GetConnectionString("OrderCon")));
}

else if (env.IsDevelopment())
{
    Console.WriteLine("Using mem InMemo");
    //Esto se hace para la inyección de dependencias
    serviceCollection.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMemo"));
}


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        }); ;



builder.Services.AddHttpClient<IShoppingCartClient, ShoppingCartClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpints =>
{
    _ = endpints.MapControllers();
});

TempOrderData.LoadData(app, app.Environment.IsProduction());


app.Run();
