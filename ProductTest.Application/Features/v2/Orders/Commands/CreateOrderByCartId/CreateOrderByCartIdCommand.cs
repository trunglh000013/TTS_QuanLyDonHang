using MediatR;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Commands.CreateOrderByCartId;

public sealed record CreateOrderByCartIdCommand(CreateOrderByCartIdRequest Request) : IRequest<CreateOrderByCartIdResponse>;

public sealed class CreateOrderByCartIdCommandHandler(
    IOrderRepositoryV2 orderRepository)
    : IRequestHandler<CreateOrderByCartIdCommand, CreateOrderByCartIdResponse>
{
    public async Task<CreateOrderByCartIdResponse> Handle(CreateOrderByCartIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await orderRepository.CreateOrderByCartIdAsync(request.Request, cancellationToken);
            return new CreateOrderByCartIdResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}