using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetProduct(request.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
            }
            //Step 1: Fetch Brand and Type
            var brand = await _productRepository.GetBrandByIdAsync(request.BrandId);
            var type = await _productRepository.GetTypeByIdAsync(request.TypeId);
            if (brand == null || type == null)
            {
                throw new ApplicationException("Invalid Brand or Type specified");
            }
            //Step 2: Mapper role
            var updatedProduct = request.ToUpdateEntity(existing, brand, type);

            // Step 3: Save the record
            return await _productRepository.UpdateProduct(updatedProduct);
        }
    }
}
