using Catalog.Responses;
using MediatR;

namespace Catalog.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; init; }
        public string Summamry { get; init; }
        public string Description { get; init; }
        public string ImageFile { get; init; }
        public string BrandId { get; init; }
        public string TypeId { get; init; }
        public decimal Price { get; init; }        
    }
}
