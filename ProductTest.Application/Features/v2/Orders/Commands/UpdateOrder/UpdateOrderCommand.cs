using MediatR;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(UpdateOrderRequest Request) : IRequest<UpdateOrderResponse>;

public sealed class UpdateOrderCommandHandler(
    IOrderRepositoryV2 orderRepository)
    : IRequestHandler<UpdateOrderCommand, UpdateOrderResponse>
{
    public async Task<UpdateOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existing = await orderRepository.GetOrderByIdAsync(new GetOrderByIdRequest { Id = request.Request.Id }, cancellationToken);
        if (existing is null)
            return new UpdateOrderResponse { Success = false };

        try
        {
            await orderRepository.UpdateOrderAsync(request.Request, cancellationToken);
            return new UpdateOrderResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}