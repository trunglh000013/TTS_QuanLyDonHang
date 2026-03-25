using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;
using AutoMapper;

namespace ProductTest.Application.Features.v2.Suppliers.Queries.GetSupplierById;

public sealed record GetSupplierByIdQuery(GetSupplierByIdRequest Request) : IRequest<GetSupplierByIdResponse>;

public sealed class GetSupplierByIdQueryHandler(
    ISupplierRepositoryV2 supplierRepository,
    IMapper mapper)
    : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdResponse>
{
    public async Task<GetSupplierByIdResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetSupplierByIdAsync(request.Request, cancellationToken);
        return new GetSupplierByIdResponse(mapper.Map<SupplierDto>(supplier));
    }
}

