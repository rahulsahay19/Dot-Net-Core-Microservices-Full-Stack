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
            //Extract Correlation id x-correlation-id
            var correlationId = HttpContext.Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            var command = dto.ToCommand();
            command.CorrelationId = Guid.Parse(correlationId);

            var result = await _mediator.Send(command);
            _logger.LogInformation($"Order created with Id: {result}, CorrelationId: {correlationId}");
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto dto)
        {
            //Extract Correlation id x-correlation-id
            var correlationId = HttpContext.Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            var command = dto.ToCommand();
            command.CorrelationId = Guid.Parse(correlationId);
            await _mediator.Send(command);
            _logger.LogInformation($"Order updated with Id: {dto.Id}, CorrelationId: {correlationId}");
            return NoContent();
        }
        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            //Extract Correlation id x-correlation-id
            var correlationId = HttpContext.Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            var command = new DeleteOrderCommand { Id = id , CorrelationId =Guid.Parse(correlationId)};
            await _mediator.Send(command);
            _logger.LogInformation($"Order deleted with Id: {id}, CorrelationId: {correlationId}");
            return NoContent();
        }
    }
}
