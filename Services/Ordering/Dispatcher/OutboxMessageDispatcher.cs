
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Data;

namespace Ordering.Dispatcher
{
    public class OutboxMessageDispatcher : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxMessageDispatcher> _logger;

        public OutboxMessageDispatcher(IServiceProvider serviceProvider, ILogger<OutboxMessageDispatcher> logger)
        {
            _serviceProvider = serviceProvider; ;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var pendingMessages = await dbContext.OutboxMessages
                                    .Where(x => x.ProcessedOn == null)
                                    .OrderBy(x => x.OccurredOn)
                                    .Take(20)
                                    .ToListAsync(stoppingToken);

                foreach (var message in pendingMessages)
                {
                    try
                    {
                        // var dynamicData = JsonConvert.DeserializeObject<dynamic>(message.Content);
                        var orderCreatedEvent = JsonConvert.DeserializeObject<OrderCreatedEvent>(message.Content);
                        await publishEndpoint.Publish(orderCreatedEvent);

                        message.ProcessedOn = DateTime.UtcNow;
                        _logger.LogInformation("Published outbox message {Id}", message.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to publish outbox message {Id}", message.Id);
                    }
                }

                await dbContext.SaveChangesAsync(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
