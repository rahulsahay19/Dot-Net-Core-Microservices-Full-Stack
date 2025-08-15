using Ordering.Entities;

namespace Ordering.Repositories
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
        Task AddOutboxMessageAsync(OutboxMessage outboxMessage);
    }
}
