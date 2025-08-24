using Common.Logging;
using Discount.Extensions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add services to the container.

//Mediatr
var assemblies = new Assembly[]
{

   Assembly.GetExecutingAssembly(), typeof(CreateDiscountHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();

//Migrate the Database
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});


app.Run();
