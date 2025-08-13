using Basket.Commands;
using Basket.Mappers;
using Basket.Queries;
using MassTransit;
using MassTransit.Transports;
using MediatR;

namespace Basket.Handlers
{
    public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CheckoutBasketCommandHandler> _logger;

        public CheckoutBasketCommandHandler(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<CheckoutBasketCommandHandler> logger)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        public async Task<Unit> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var basketResponse = await _mediator.Send(new GetBasketByUserNameQuery(dto.UserName), cancellationToken);
            if(basketResponse is null || !basketResponse.Items.Any())
            {
                throw new InvalidOperationException("Basket not found or empty");
            }
            var basket = basketResponse.ToEntity();

            //Map
            var evt = dto.ToBasketCheckoutEvent(basket);
            _logger.LogInformation("Publishing BasketCheckoutEvent for {User}", basket.UserName);
            await _publishEndpoint.Publish(evt, cancellationToken);
            //delete the basket
            await _mediator.Send(new DeleteBasketByUserNameCommand(dto.UserName), cancellationToken);
            return Unit.Value;
        }
    }
}
