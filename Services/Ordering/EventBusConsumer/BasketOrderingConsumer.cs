using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Mappers;

namespace Ordering.EventBusConsumer
{
    public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketOrderingConsumer> _logger;

        public BasketOrderingConsumer(IMediator mediator, ILogger<BasketOrderingConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            using var scope = _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}", context.Message.CorrelationId);
            var command = context.Message.ToCheckoutOrderCommand();
            var result = await _mediator.Send(command);
            _logger.LogInformation("Basket Checkout Event completed Successfully.");
        }
    }
}
