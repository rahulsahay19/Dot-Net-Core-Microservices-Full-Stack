using Discount.DTOs;
using Discount.Mappers;
using Discount.Queries;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            //Validate the input
            if (string.IsNullOrWhiteSpace(request.productName))
            {
                var validationErrors = new Dictionary<string, string>
                {
                    { "ProductName", "Product name must not be empty."}
                };
            }
            //Fetch from repo
            var coupon = await _discountRepository.GetDiscount(request.productName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.productName} not found"));
            }
            //Mapping 
            return coupon.ToDto();
        }
    }
}
