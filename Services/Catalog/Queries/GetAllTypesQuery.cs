using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetAllTypesQuery : IRequest<IList<TypesResponse>>
    {
    }
}
