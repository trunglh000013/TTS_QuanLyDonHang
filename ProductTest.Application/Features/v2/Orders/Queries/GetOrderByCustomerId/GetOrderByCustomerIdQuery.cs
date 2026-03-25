using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Queries.GetOrderByCustomerId;

public sealed record GetOrderByCustomerIdQuery(GetOrderByCustomerIdRequest Request) : IRequest<GetOrderByCustomerIdResponse>;

public sealed class GetOrderByCustomerIdQueryHandler(
    IOrderRepositoryV2 orderRepository,
    IMapper mapper)
    : IRequestHandler<GetOrderByCustomerIdQuery, GetOrderByCustomerIdResponse>
{
    public async Task<GetOrderByCustomerIdResponse> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrderByCustomerIdAsync(request.Request, cancellationToken);
        return new GetOrderByCustomerIdResponse
        {
            Items = mapper.Map<List<OrderDto>>(orders),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = orders.Count
        };
    }
}
