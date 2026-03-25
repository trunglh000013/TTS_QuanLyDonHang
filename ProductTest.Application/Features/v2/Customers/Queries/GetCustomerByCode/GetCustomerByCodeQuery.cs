using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;

namespace ProductTest.Application.Features.v2.Customers.Queries.GetCustomerByCode;

public sealed record GetCustomerByCodeQuery(GetCustomerByCodeRequest Request) : IRequest<GetCustomerByCodeResponse>;

public sealed class GetCustomerByCodeQueryHandler(
    ICustomerRepositoryV2 customerRepository,
    IMapper mapper)
    : IRequestHandler<GetCustomerByCodeQuery, GetCustomerByCodeResponse>
{
    public async Task<GetCustomerByCodeResponse> Handle(GetCustomerByCodeQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetCustomerByCodeAsync(request.Request, cancellationToken);
        return new GetCustomerByCodeResponse { Customer = mapper.Map<CustomerDto>(customer) };
    }
}
