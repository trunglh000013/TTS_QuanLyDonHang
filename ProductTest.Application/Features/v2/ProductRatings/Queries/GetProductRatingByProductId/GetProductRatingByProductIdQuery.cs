using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;
using AutoMapper;

namespace ProductTest.Application.Features.v2.ProductRatings.Queries.GetProductRatingByProductId;

public sealed record GetProductRatingByProductIdQuery(GetProductRatingByProductIdRequest Request) : IRequest<GetProductRatingByProductIdResponse>;

public sealed class GetProductRatingByProductIdQueryHandler(
    IProductRatingRepositoryV2 productRatingRepository,
    IMapper mapper)
    : IRequestHandler<GetProductRatingByProductIdQuery, GetProductRatingByProductIdResponse>
{
    public async Task<GetProductRatingByProductIdResponse> Handle(GetProductRatingByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productRating = await productRatingRepository.GetProductRatingsByProductIdAsync(request.Request, cancellationToken);
        return new GetProductRatingByProductIdResponse
        {
            Items = mapper.Map<List<ProductRatingDto>>(productRating),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = productRating.Count,
        };
    }
}