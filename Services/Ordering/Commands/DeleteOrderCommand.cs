using MediatR;

namespace Ordering.Commands
{
    public record DeleteOrderCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
