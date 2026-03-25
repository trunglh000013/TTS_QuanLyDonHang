using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;
using AutoMapper;

namespace ProductTest.Application.Features.v2.Suppliers.Queries.GetSupplierByProductId;

public sealed record GetSupplierByProductIdQuery(GetSupplierByProductIdRequest Request) : IRequest<GetSupplierByProductIdResponse>;

public sealed class GetSupplierByProductIdQueryHandler(
    ISupplierRepositoryV2 supplierRepository,
    IMapper mapper)
    : IRequestHandler<GetSupplierByProductIdQuery, GetSupplierByProductIdResponse>
{
    public async Task<GetSupplierByProductIdResponse> Handle(GetSupplierByProductIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetSuppliersByProductIdAsync(request.Request, cancellationToken);
        return new GetSupplierByProductIdResponse(mapper.Map<SupplierDto>(supplier));
    }
}

