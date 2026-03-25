using MediatR;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;

namespace ProductTest.Application.Features.v2.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(UpdateCustomerRequest Request) : IRequest<UpdateCustomerResponse>;

public sealed class UpdateCustomerCommandHandler(
    ICustomerRepositoryV2 customerRepository)
    : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{
    public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existing = await customerRepository.GetCustomerByIdAsync(new GetCustomerByIdRequest { Id = request.Request.Id }, cancellationToken);
        if (existing is null)
            return new UpdateCustomerResponse { Success = false };

        try
        {
            await customerRepository.UpdateCustomerAsync(request.Request, cancellationToken);
            return new UpdateCustomerResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
