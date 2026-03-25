using MediatR;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(DeleteOrderRequest Request) : IRequest<DeleteOrderResponse>;

public sealed class DeleteOrderCommandHandler(
    IOrderRepositoryV2 orderRepository)
    : IRequestHandler<DeleteOrderCommand, DeleteOrderResponse>
{
    public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var existing = await orderRepository.GetOrderByIdAsync(
            new GetOrderByIdRequest { Id = request.Request.Id }, cancellationToken);
        if (existing is null)
            return new DeleteOrderResponse { Success = false };

        try
        {
            await orderRepository.DeleteOrderAsync(request.Request, cancellationToken);
            return new DeleteOrderResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}