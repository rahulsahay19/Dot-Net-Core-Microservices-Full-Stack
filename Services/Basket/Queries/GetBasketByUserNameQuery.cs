using Basket.Responses;
using MediatR;

namespace Basket.Queries
{
    public record GetBasketByUserNameQuery(string UserName) : IRequest<ShoppingCartResponse>; 
   
}
