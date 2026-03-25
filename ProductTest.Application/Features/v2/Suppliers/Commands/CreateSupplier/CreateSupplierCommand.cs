using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;

namespace ProductTest.Application.Features.v2.Suppliers.Commands.CreateSupplier;

public sealed record CreateSupplierCommand(CreateSupplierRequest Request) : IRequest<CreateSupplierResponse>;

public sealed class CreateSupplierCommandHandler(
    ISupplierRepositoryV2 supplierRepository)
    : IRequestHandler<CreateSupplierCommand, CreateSupplierResponse>
{
    public async Task<CreateSupplierResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        await supplierRepository.CreateSupplierAsync(request.Request, cancellationToken);
        return new CreateSupplierResponse(true);
    }
}

