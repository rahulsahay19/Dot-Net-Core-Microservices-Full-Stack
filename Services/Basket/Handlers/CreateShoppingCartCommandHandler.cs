using Basket.Commands;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // Convert Command to domain entity
            var shoppingCartEntity = request.ToEntity();
            //Save to Redis
            var updatedCart = await _basketRepository.UpsertBasket(shoppingCartEntity);
            //Convert back to Response
            return updatedCart.ToResponse();
        }
    }
}
