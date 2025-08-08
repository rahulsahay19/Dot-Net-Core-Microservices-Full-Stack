using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ordering.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order) =>
            new(order.Id, order.UserName!, order.TotalPrice ?? 0, order.FirstName!, order.LastName!,
                order.EmailAddress!, order.AddressLine!, order.Country!, order.State!, order.ZipCode!,
                order.CardName!,order.CardNumber!, order.Expiration!, order.Cvv!, order.PaymentMethod ?? 0);

        public static Order ToEntity(this CheckoutOrderCommand command)
        {
            return new Order
            {
                UserName = command.UserName,
                TotalPrice = command.TotalPrice,
                FirstName = command.FirstName,
                LastName = command.LastName,
                EmailAddress = command.EmailAddress,
                AddressLine = command.AddressLine,
                Country = command.Country,
                State = command.State,
                ZipCode = command.ZipCode,
                CardName = command.CardName,
                CardNumber = command.CardNumber,
                Expiration = command.Expiration,
                Cvv = command.Cvv,
                PaymentMethod =command.PaymentMethod
            };
        }

        public static CheckoutOrderCommand ToCommand(this CreateOrderDto dto)
        {
            return new CheckoutOrderCommand
            {
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod
            };
        }

        public static UpdateOrderCommand ToCommand(this OrderDto dto)
        {
            return new UpdateOrderCommand
            {
                Id = dto.Id,
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod
            };
        }
        public static void MapUpdate(this Order orderToUpdate, UpdateOrderCommand request)
        {
            orderToUpdate.UserName = request.UserName;
            orderToUpdate.TotalPrice = request.TotalPrice;
            orderToUpdate.FirstName = request.FirstName;
            orderToUpdate.LastName = request.LastName;
            orderToUpdate.EmailAddress = request.EmailAddress;
            orderToUpdate.AddressLine = request.AddressLine;
            orderToUpdate.Country = request.Country;
            orderToUpdate.State = request.State;
            orderToUpdate.ZipCode = request.ZipCode;
            orderToUpdate.CardName = request.CardName;
            orderToUpdate.CardNumber = request.CardNumber;
            orderToUpdate.Expiration = request.Expiration;
            orderToUpdate.Cvv = request.Cvv;
            orderToUpdate.PaymentMethod = request.PaymentMethod;
        }
    }
}
