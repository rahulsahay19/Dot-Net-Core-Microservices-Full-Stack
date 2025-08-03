using MediatR;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class GetOrderListHandler : IRequestHandler<GetOrderList, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderListHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDto>> Handle(GetOrderList request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserName(request.UserName);
            return orders.Select(o => o.ToDto()).ToList();
        }
    }
}
