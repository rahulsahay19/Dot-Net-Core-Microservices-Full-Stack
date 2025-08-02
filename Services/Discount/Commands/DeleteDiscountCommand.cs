using MediatR;

namespace Discount.Commands
{
    public record DeleteDiscountCommand(string ProductName): IRequest<bool>;
    
}
