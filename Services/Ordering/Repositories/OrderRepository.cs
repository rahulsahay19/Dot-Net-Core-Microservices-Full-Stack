using Microsoft.EntityFrameworkCore;
using Ordering.Data;
using Ordering.Entities;

namespace Ordering.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext): base(dbContext) { }
        
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                .AsNoTracking() //For better perfomance
                .Where(o=>o.UserName == userName)
                .ToListAsync();
            return orderList;
        }
    }
}
