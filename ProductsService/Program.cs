using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using ProductsService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment; // Get the IWebHostEnvironment instance
var Configuration = builder.Configuration;// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
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

if (env.IsProduction())
{
    //SI LO DEJO ASI VA A ESTAR POR DEFECTO EN DEVELOPMENT ENVIORONMENT
    Console.WriteLine("Using SQL Server");
    serviceCollection.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("ProductsCon")));
}
else if (env.IsDevelopment())
{
    Console.WriteLine("Using mem InMemo");
    //Esto se hace para la inyección de dependencias
    serviceCollection.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMemo"));
}

builder.Services.AddScoped<IProductRepo, ProductRepoImp>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

var app = builder.Build();

PrepData.LoadData(app, env.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);


app.UseEndpoints(endpints =>
{
    _ = endpints.MapControllers();
});

app.Run();