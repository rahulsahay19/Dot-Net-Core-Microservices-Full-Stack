using MediatR;

namespace Catalog.Commands
{
    public record DeleteProductByIdCommand(string Id): IRequest<bool>;
   
}
