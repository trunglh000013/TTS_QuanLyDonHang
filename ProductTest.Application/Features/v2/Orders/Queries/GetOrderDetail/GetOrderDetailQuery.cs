using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Queries.GetOrderDetail;

public sealed record GetOrderDetailQuery(GetOrderDetailRequest Request) : IRequest<GetOrderDetailResponse>;

public sealed class GetOrderDetailQueryHandler(
    IOrderRepositoryV2 orderRepository,
    IMapper mapper)
    : IRequestHandler<GetOrderDetailQuery, GetOrderDetailResponse>
{
    public async Task<GetOrderDetailResponse> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await orderRepository.GetOrderDetailAsync(request.Request, cancellationToken);
        return new GetOrderDetailResponse
        {
            Items = mapper.Map<List<OrderItemDto>>(orderItems),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = orderItems.Count
        };
    }
}
