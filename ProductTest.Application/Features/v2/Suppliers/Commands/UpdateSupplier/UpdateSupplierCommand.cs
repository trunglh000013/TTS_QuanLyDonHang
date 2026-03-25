using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;

namespace ProductTest.Application.Features.v2.Suppliers.Commands.UpdateSupplier;

public sealed record UpdateSupplierCommand(UpdateSupplierRequest Request) : IRequest<UpdateSupplierResponse>;

public sealed class UpdateSupplierCommandHandler(
    ISupplierRepositoryV2 supplierRepository)
    : IRequestHandler<UpdateSupplierCommand, UpdateSupplierResponse>
{
    public async Task<UpdateSupplierResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetSupplierByIdAsync(new GetSupplierByIdRequest { Id = request.Request.Id }, cancellationToken);

        if (supplier is null)
            return new UpdateSupplierResponse(false);

        await supplierRepository.UpdateSupplierAsync(request.Request, cancellationToken);
        return new UpdateSupplierResponse(true);
    }
}

