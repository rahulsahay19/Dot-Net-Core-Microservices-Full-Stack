using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {
    }
}
