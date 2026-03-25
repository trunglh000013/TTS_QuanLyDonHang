using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(GetOrderByIdRequest Request) : IRequest<GetOrderByIdResponse>;

public sealed class GetOrderByIdQueryHandler(
    IOrderRepositoryV2 orderRepository,
    IMapper mapper)
    : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
{
    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByIdAsync(request.Request, cancellationToken);
        return new GetOrderByIdResponse
        {
            Order = order is null ? null : mapper.Map<OrderDto>(order)
        };
    }
}
