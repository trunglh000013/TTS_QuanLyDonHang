using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Queries.GetAllOrder;

public sealed record GetAllOrderQuery(GetAllOrderRequest Request) : IRequest<GetAllOrderResponse>;

public sealed class GetAllOrderQueryHandler(
    IOrderRepositoryV2 orderRepository,
    IMapper mapper)
    : IRequestHandler<GetAllOrderQuery, GetAllOrderResponse>
{
    public async Task<GetAllOrderResponse> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllOrdersAsync(request.Request, cancellationToken);
        return new GetAllOrderResponse
        {
            Items = mapper.Map<List<OrderDto>>(orders),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = orders.Count
        };
    }
}
