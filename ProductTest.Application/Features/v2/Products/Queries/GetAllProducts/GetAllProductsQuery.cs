using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v2.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery(GetAllProductRequest Request) : IRequest<GetAllProductResponse>;

public sealed class GetAllProductsQueryHandler(IProductRepositoryV2 productRepository, IMapper mapper) : IRequestHandler<GetAllProductsQuery, GetAllProductResponse>
{
    public async Task<GetAllProductResponse> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(
            request.Request,
            cancellationToken);

        return new GetAllProductResponse { Items = mapper.Map<List<ProductDto>>(products), PageNumber = request.Request.PageNumber, PageSize = request.Request.PageSize, TotalCount = products.Count };
    }
}
