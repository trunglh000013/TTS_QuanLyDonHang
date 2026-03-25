using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v2.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(GetProductByIdRequest Request) : IRequest<GetProductByIdResponse>;

public sealed class GetProductByIdQueryHandler(
    IProductRepositoryV2 productRepository,
    IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Request, cancellationToken);
        return new GetProductByIdResponse { Product = mapper.Map<ProductDto>(product) };
    }
}