using Basket.Commands;
using Basket.DTOs;
using Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get: api/v1/basket/{userName}
        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCartDto>> GetBasket(string userName)
        { 
            var query = new GetBasketByUserNameQuery(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        //Post: api/v1/basket
        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand command)
        { 
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //Delete: api/v1/basket/{userName}
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        { 
            var cmd = new DeleteBasketByUserNameCommand(userName);
            await _mediator.Send(cmd);
            return Ok();
        }
    }
}
