using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v1.Products.Queries.SearchProducts;

public sealed record SearchProductsQuery(
    SearchProductRequest Request) : IRequest<SearchProductResponse>;

public sealed class SearchProductsQueryHandler(IProductRepositoryV1 productRepository, IMapper mapper) : IRequestHandler<SearchProductsQuery, SearchProductResponse>
{
    public async Task<SearchProductResponse> Handle(
        SearchProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.SearchAsync(
            request.Request,
            cancellationToken);

        return new SearchProductResponse { Items = mapper.Map<List<ProductDto>>(products), PageNumber = request.Request.PageNumber, PageSize = request.Request.PageSize, TotalCount = products.Count };
    }
}
