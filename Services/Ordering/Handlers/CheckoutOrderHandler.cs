using MediatR;
using Ordering.Commands;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CheckoutOrderHandler> _logger;

        public CheckoutOrderHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = request.ToEntity();
            var generatedOrder = await _orderRepository.AddAsync(orderEntity);
            var outboxMessage = OrderMapper.ToOutboxMessage(generatedOrder);
            await _orderRepository.AddOutboxMessageAsync(outboxMessage);
            _logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created with outbox message.");
            return generatedOrder.Id;
        }
    }
}
