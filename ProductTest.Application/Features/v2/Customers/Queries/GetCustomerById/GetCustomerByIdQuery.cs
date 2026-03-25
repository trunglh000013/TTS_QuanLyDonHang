using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;

namespace ProductTest.Application.Features.v2.Customers.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(GetCustomerByIdRequest Request) : IRequest<GetCustomerByIdResponse>;

public sealed class GetCustomerByIdQueryHandler(
    ICustomerRepositoryV2 customerRepository,
    IMapper mapper)
    : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetCustomerByIdAsync(request.Request, cancellationToken);
        return new GetCustomerByIdResponse { Customer = mapper.Map<CustomerDto>(customer) };
    }
}
