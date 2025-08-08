using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;

namespace Ordering.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{userName}", Name = "GetOrdersByUserName")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([FromRoute] string userName)
        {
            var query = new GetOrderList(userName);
            var orders = await _mediator.Send(query);
            _logger.LogInformation($"$Orders fetched for user: {userName}");
            return Ok(orders);
        }

        //testing purpose 
        [HttpPost(Name = "CheckoutOrder")]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CreateOrderDto dto)
        {
            var command = dto.ToCommand();
            var result = await _mediator.Send(command);
            _logger.LogInformation($"Order created with Id: {result}");
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto dto)
        {
            var command = dto.ToCommand();
            await _mediator.Send(command);
            _logger.LogInformation($"Order updated with Id: {dto.Id}");
            return NoContent();
        }
        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        { 
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            _logger.LogInformation($"Order deleted with Id: {id}");
            return NoContent();
        }
    }
}
