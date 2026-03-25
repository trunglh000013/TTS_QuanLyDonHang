using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;

namespace ProductTest.Application.Features.v2.Customers.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery(GetAllCustomerRequest Request) : IRequest<GetAllCustomersResponse>;

public sealed class GetAllCustomersQueryHandler(
    ICustomerRepositoryV2 customerRepositoryV2,
    IMapper mapper)
    : IRequestHandler<GetAllCustomersQuery, GetAllCustomersResponse>
{
    public async Task<GetAllCustomersResponse> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var items = await customerRepositoryV2.GetAllCustomersAsync(request.Request, cancellationToken);

        return new GetAllCustomersResponse
        {
            Items = mapper.Map<List<CustomerDto>>(items),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = items.Count
        };
    }
}
