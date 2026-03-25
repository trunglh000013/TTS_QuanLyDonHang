using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;
using AutoMapper;

namespace ProductTest.Application.Features.v2.Suppliers.Queries.GetSupplierByCode;

public sealed record GetSupplierByCodeQuery(GetSupplierByCodeRequest Request) : IRequest<GetSupplierByCodeResponse>;

public sealed class GetSupplierByCodeQueryHandler(
    ISupplierRepositoryV2 supplierRepository,
    IMapper mapper)
    : IRequestHandler<GetSupplierByCodeQuery, GetSupplierByCodeResponse>
{
    public async Task<GetSupplierByCodeResponse> Handle(GetSupplierByCodeQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetSupplierByCodeAsync(request.Request, cancellationToken);
        return new GetSupplierByCodeResponse(mapper.Map<SupplierDto>(supplier));
    }
}

