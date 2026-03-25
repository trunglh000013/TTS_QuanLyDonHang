using MediatR;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;

namespace ProductTest.Application.Features.v2.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(CreateCustomerRequest Request) : IRequest<CreateCustomerResponse>;

public sealed class CreateCustomerCommandHandler(
    ICustomerRepositoryV2 customerRepository)
    : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await customerRepository.CreateCustomerAsync(request.Request, cancellationToken);
            return new CreateCustomerResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
