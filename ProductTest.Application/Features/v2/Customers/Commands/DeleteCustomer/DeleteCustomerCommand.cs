using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;

namespace ProductTest.Application.Features.v2.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(DeleteCustomerRequest Request) : IRequest<DeleteCustomerResponse>;

public sealed class DeleteCustomerCommandHandler(
    ICustomerRepositoryV2 customerRepository,
    IMapper mapper)
    : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResponse>
{
    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var existing = await customerRepository.GetCustomerByIdAsync(new GetCustomerByIdRequest { Id = request.Request.Id }, cancellationToken);
        if (existing is null)
            return new DeleteCustomerResponse { Success = false };

        try
        {
            await customerRepository.DeleteCustomerAsync(request.Request, cancellationToken);
            return new DeleteCustomerResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
