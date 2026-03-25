using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;
using AutoMapper;

namespace ProductTest.Application.Features.v2.ProductRatings.Queries.GetProductRatingByCode;

public sealed record GetProductRatingByCodeQuery(GetProductRatingByCodeRequest Request) : IRequest<GetProductRatingByCodeResponse>;

public sealed class GetProductRatingByCodeQueryHandler(
    IProductRatingRepositoryV2 productRatingRepository,
    IMapper mapper)
    : IRequestHandler<GetProductRatingByCodeQuery, GetProductRatingByCodeResponse>
{
    public async Task<GetProductRatingByCodeResponse> Handle(GetProductRatingByCodeQuery request, CancellationToken cancellationToken)
    {
        var productRating = await productRatingRepository.GetProductRatingByCodeAsync(request.Request, cancellationToken);
        return new GetProductRatingByCodeResponse
        {
            ProductRating = mapper.Map<ProductRatingDto>(productRating)
        };
    }
}
