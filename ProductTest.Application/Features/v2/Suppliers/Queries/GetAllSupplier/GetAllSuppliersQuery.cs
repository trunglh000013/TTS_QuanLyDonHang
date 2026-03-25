using MediatR;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;
using AutoMapper;

namespace ProductTest.Application.Features.v2.Suppliers.Queries.GetAllSupplier;

public sealed record GetAllSupplierQuery(GetAllSupplierRequest Request) : IRequest<GetAllSupplierResponse>;

public sealed class GetAllSupplierQueryHandler(
    ISupplierRepositoryV2 supplierRepository,
    IMapper mapper)
    : IRequestHandler<GetAllSupplierQuery, GetAllSupplierResponse>
{
    public async Task<GetAllSupplierResponse> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await supplierRepository.GetAllSuppliersAsync(request.Request, cancellationToken);

        return new GetAllSupplierResponse
        {
            Items = mapper.Map<List<SupplierDto>>(suppliers),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = suppliers.Count()
        };
    }
}