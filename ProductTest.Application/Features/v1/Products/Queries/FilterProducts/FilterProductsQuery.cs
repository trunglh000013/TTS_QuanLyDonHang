using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v1.Products.Queries.FilterProducts;

public sealed record FilterProductsQuery(
    FilterProductRequest Request) : IRequest<FilterProductResponse>;

public sealed class FilterProductsQueryHandler(IProductRepositoryV1 productRepository, IMapper mapper) : IRequestHandler<FilterProductsQuery, FilterProductResponse>
{
    public async Task<FilterProductResponse> Handle(
        FilterProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.FilterAsync(
            request.Request,
            cancellationToken);

        return new FilterProductResponse { Items = mapper.Map<List<ProductDto>>(products), PageNumber = request.Request.PageNumber, PageSize = request.Request.PageSize, TotalCount = products.Count };
    }
}
