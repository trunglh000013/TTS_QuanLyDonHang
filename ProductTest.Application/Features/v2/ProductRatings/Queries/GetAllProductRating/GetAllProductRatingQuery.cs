using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;

namespace ProductTest.Application.Features.v2.ProductRatings.Queries.GetAllProductRating;

public sealed record GetAllProductRatingQuery(GetAllProductRatingRequest Request) : IRequest<GetAllProductRatingResponse>;

public sealed class GetAllProductRatingQueryHandler(
    IProductRatingRepositoryV2 productRatingRepository,
    IMapper mapper)
    : IRequestHandler<GetAllProductRatingQuery, GetAllProductRatingResponse>
{
    public async Task<GetAllProductRatingResponse> Handle(GetAllProductRatingQuery request, CancellationToken cancellationToken)
    {
        var result = await productRatingRepository.GetAllProductRatingsAsync(request.Request, cancellationToken);
        return new GetAllProductRatingResponse
        {
            Items = mapper.Map<List<ProductRatingDto>>(result),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = result.Count
        };
    }
}
