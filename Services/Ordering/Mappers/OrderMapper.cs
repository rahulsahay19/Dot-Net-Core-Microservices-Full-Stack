using Ordering.DTOs;
using Ordering.Entities;

namespace Ordering.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order) =>
            new(order.Id, order.UserName!, order.TotalPrice ?? 0, order.FirstName!, order.LastName!,
                order.EmailAddress!, order.AddressLine!, order.Country!, order.State!, order.ZipCode!,
                order.CardName!,order.CardNumber!, order.Expiration!, order.Cvv!, order.PaymentMethod ?? 0);
    }
}
