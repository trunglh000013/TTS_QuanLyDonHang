using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;
using AutoMapper;

namespace ProductTest.Application.Features.v2.ProductRatings.Queries.GetProductRatingByProductId;

public sealed record GetProductRatingByIdQuery(GetProductRatingByIdRequest Request) : IRequest<GetProductRatingByIdResponse>;

public sealed class GetProductRatingByIdQueryHandler(
    IProductRatingRepositoryV2 productRatingRepository,
    IMapper mapper)
    : IRequestHandler<GetProductRatingByIdQuery, GetProductRatingByIdResponse>
{
    public async Task<GetProductRatingByIdResponse> Handle(GetProductRatingByIdQuery request, CancellationToken cancellationToken)
    {
        var productRating = await productRatingRepository.GetProductRatingByIdAsync(request.Request, cancellationToken);
        return new GetProductRatingByIdResponse
        {
            ProductRating = mapper.Map<ProductRatingDto>(productRating)
        };
    }
}
