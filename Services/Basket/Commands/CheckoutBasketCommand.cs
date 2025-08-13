using Basket.DTOs;
using MediatR;

namespace Basket.Commands
{
    public record CheckoutBasketCommand(BasketCheckoutDto Dto) : IRequest<Unit>;
   
}
