using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        { 
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductByProductName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await _mediator.Send(query);
            if (result == null || !result.Any())
            {
                return NotFound();
            }

            var dtoList = result.Select(p=>p.ToDto()).ToList();
            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        { 
            var command = new DeleteProductByIdCommand(id);
            var result = await _mediator.Send(command);
            if (!result) 
            {
                return NotFound(); //Product wasn't found or maybe deletion failed
            }

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, UpdateProductDto updateProductDto)
        {
            var command = updateProductDto.ToCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("brand/{brand}", Name = "GetProductsByBrandName")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByBrand(string brand)
        {
            //First get the products
            var query = new GetProductByBrandQuery(brand);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
