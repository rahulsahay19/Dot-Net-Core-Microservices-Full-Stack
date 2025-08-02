using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductByIdQuery(string Id) : IRequest<ProductResponse>
    {
    }
}
