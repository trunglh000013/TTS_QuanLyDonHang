using MediatR;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(CreateOrderRequest Request) : IRequest<CreateOrderResponse>;

public sealed class CreateOrderCommandHandler(
    IOrderRepositoryV2 orderRepository)
    : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await orderRepository.CreateOrderAsync(request.Request, cancellationToken);
            return new CreateOrderResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}