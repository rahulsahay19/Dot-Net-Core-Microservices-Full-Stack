using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded!!!");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new()
                {
                    UserName = "rahul",
                    FirstName = "Rahul",
                    LastName = "Sahay",
                    EmailAddress = "rahulsahay@eCommerce.net",
                    AddressLine = "Bangalore",
                    Country = "India",
                    TotalPrice = 750,
                    State = "KA",
                    ZipCode = "560001",

                    CardName = "Visa",
                    CardNumber = "4111111111111111",
                    CreatedBy = "Rahul",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "Rahul",
                    LastModifiedDate = new DateTime(),
                }
            };
        }
    }
}
