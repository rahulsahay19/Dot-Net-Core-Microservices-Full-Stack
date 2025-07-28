using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Responses;
using Catalog.Specifications;

namespace Catalog.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            if (product == null) return null;
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Summary = product.Summamry,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Price = product.Price,
                Brand = product.Brand,
                Type = product.Type,
                CreatedDate = product.CreatedDate
            };
        }

        public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products) =>
            products.Select(p=>p.ToResponse());

        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)  =>
            new Pagination<ProductResponse>(
                    pagination.PageIndex,
                    pagination.PageSize,
                    pagination.Count,
                    pagination.Data.Select(p => p.ToResponse()).ToList()
                );

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type) =>
            new Product
            {
                Name = command.Name,
                Summamry = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existing, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Id = existing.Id,
                Name = command.Name,
                Summamry = command.Summamry,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = existing.CreatedDate //Preserving the existing date
            };
        }
        public static ProductDto ToDto(this ProductResponse product)
        {
            if (product == null) return null;
            return new ProductDto
                (
                    product.Id,
                    product.Name,
                    product.Summary,
                    product.Description,
                    product.ImageFile,
                    new BrandDto(product.Brand.Id, product.Brand.Name),
                    new TypeDto(product.Type.Id, product.Type.Name),
                    product.Price,
                    DateTimeOffset.UtcNow
                );
        }

        public static UpdateProductCommand ToCommand(this UpdateProductDto dto, string id)
        {
            return new UpdateProductCommand
            {
                Id = id,
                Name =  dto.Name,
                Summamry = dto.Summary,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                Price = dto.Price,
                BrandId = dto.BrandId,
                TypeId = dto.TypeId
            };
        }
    }
}
