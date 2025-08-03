using Microsoft.EntityFrameworkCore;
using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options): base(options) 
        {
            
        }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "Rahul"; //TODO: Replace this with User from IUserService
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "Rahul";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
