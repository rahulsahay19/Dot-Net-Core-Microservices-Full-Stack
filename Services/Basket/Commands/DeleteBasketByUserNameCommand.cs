using MediatR;

namespace Basket.Commands
{
    public record DeleteBasketByUserNameCommand(string userName): IRequest<Unit>;
}
