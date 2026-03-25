using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;

namespace ProductTest.Application.Features.v2.Orders.Queries.GetOrderByCode;

public sealed record GetOrderByCodeQuery(GetOrderByCodeRequest Request) : IRequest<GetOrderByCodeResponse>;

public sealed class GetOrderByCodeQueryHandler(
    IOrderRepositoryV2 orderRepository,
    IMapper mapper)
    : IRequestHandler<GetOrderByCodeQuery, GetOrderByCodeResponse>
{
    public async Task<GetOrderByCodeResponse> Handle(GetOrderByCodeQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByCodeAsync(request.Request, cancellationToken);
        return new GetOrderByCodeResponse
        {
            Order = order is null ? null : mapper.Map<OrderDto>(order)
        };
    }
}
