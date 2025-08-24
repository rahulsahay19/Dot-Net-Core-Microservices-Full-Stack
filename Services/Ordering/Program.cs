using Common.Logging;
using EventBus.Messages.Common;
using MassTransit;
using Ordering.Data;
using Ordering.Dispatcher;
using Ordering.EventBusConsumer;
using Ordering.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//Application Services
builder.Services.AddApplicationServices();

//Infra services
builder.Services.AddInfraServices(builder.Configuration);

//Register Outbox Message Dispatcher
builder.Services.AddHostedService<OutboxMessageDispatcher>();
//Mass Transit
builder.Services.AddMassTransit(config =>
{
    //Mark as consumer
    config.AddConsumer<BasketOrderingConsumer>();
    config.AddConsumer<PaymentCompletedConsumer>();
    config.AddConsumer<PaymentFailedConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //provide the queue name with consumer settings
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
        });
        //Payment completed Event
        cfg.ReceiveEndpoint(EventBusConstant.PaymentCompletedQueue, e =>
        {
            e.ConfigureConsumer<PaymentCompletedConsumer>(ctx);
        });
        //Payment failed event
        cfg.ReceiveEndpoint(EventBusConstant.PaymentFailedQueue, e =>
        {
            e.ConfigureConsumer<PaymentFailedConsumer>(ctx);
        });
    });
});

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
//Migration
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Enable swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
