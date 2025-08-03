using MediatR;
using Ordering.DTOs;

namespace Ordering.Queries
{
    public record GetOrderList(string UserName): IRequest<List<OrderDto>>;
}
