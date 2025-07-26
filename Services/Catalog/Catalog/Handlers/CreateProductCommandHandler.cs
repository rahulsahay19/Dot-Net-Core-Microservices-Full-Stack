using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Fetch Brand and Type from repository
            var brand = await _productRepository.GetBrandByIdAsync(request.BrandId);
            var type = await _productRepository.GetTypeByIdAsync(request.TypeId);

            if (brand == null || type == null)
            {
                throw new ApplicationException("Invalid Brand or Type specified");
            }
            //Match to entity
            var productEntity = request.ToEntity(brand, type);
            var newProduct = await _productRepository.CreateProduct(productEntity);
            return newProduct.ToResponse();
        }
    }
}
