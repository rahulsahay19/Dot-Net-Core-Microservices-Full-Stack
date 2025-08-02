using Discount.Commands;
using Discount.DTOs;
using Discount.Entities;

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
    }
}
