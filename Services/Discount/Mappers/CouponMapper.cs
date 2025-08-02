using Discount.Commands;
using Discount.DTOs;
using Discount.Entities;
using Discount.Grpc.Protos;

namespace Discount.Mappers
{
    public static class CouponMapper
    {
        public static CouponDto ToDto(this Coupon coupon)
        {
            return new CouponDto(
                    coupon.Id,
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount
                );
        }
        public static Coupon ToEntity(this CreateDiscountCommand command)
        {
            return new Coupon
            {
                ProductName = command.ProductName,
                Description = command.Description,
                Amount = command.Amount
            };
        }
        public static Coupon ToEntity(this UpdateDiscountCommand command)
        {
            return new Coupon
            {
                Id = command.Id,
                ProductName = command.ProductName,
                Description = command.Description,
                Amount = command.Amount
            };
        }
        public static CouponModel ToModel(this CouponDto dto)
        {
            return new CouponModel
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
                Description = dto.Description,
                Amount = dto.Amount
            };
        }
        public static CreateDiscountCommand ToCreateCommand(this CouponModel model)
        {
            return new CreateDiscountCommand(
                    model.ProductName,
                    model.Description,
                    model.Amount
                );
        }
        public static UpdateDiscountCommand ToUpdateCommand(this CouponModel model)
        {
            return new UpdateDiscountCommand(
                    model.Id,
                    model.ProductName,
                    model.Description,
                    model.Amount
                );
        }
    }
}
