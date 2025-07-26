using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Mappers
{
    public static class TypeMapper
    {
        public static TypesResponse ToResponse(this ProductType type)
        {
            return new TypesResponse
            {
                Id = type.Id,
                Name = type.Name,
            };
        }

        public static IList<TypesResponse> ToResponseList(this IEnumerable<ProductType> types)
        {
            return types.Select(b => b.ToResponse()).ToList();
        }
    }
}
