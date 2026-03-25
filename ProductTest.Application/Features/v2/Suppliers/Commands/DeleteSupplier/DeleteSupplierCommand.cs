using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;

namespace ProductTest.Application.Features.v2.Suppliers.Commands.DeleteSupplier;

public sealed record DeleteSupplierCommand(DeleteSupplierRequest Request) : IRequest<DeleteSupplierResponse>;

public sealed class DeleteSupplierCommandHandler(
    ISupplierRepositoryV2 supplierRepository)
    : IRequestHandler<DeleteSupplierCommand, DeleteSupplierResponse>
{
    public async Task<DeleteSupplierResponse> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetSupplierByIdAsync(new GetSupplierByIdRequest { Id = request.Request.Id }, cancellationToken);

        if (supplier is null)
            return new DeleteSupplierResponse(false);

        await supplierRepository.DeleteSupplierAsync(request.Request, cancellationToken);
        return new DeleteSupplierResponse(true);
    }
}

