using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductByBrandQuery(string BrandName): IRequest<IList<ProductResponse>>;
}
