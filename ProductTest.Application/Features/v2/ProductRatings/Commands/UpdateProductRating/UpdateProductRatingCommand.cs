using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;

namespace ProductTest.Application.Features.v2.ProductRatings.Commands.UpdateProductRating;

public sealed record UpdateProductRatingCommand(UpdateProductRatingRequest Request) : IRequest<UpdateProductRatingResponse>;

public sealed class UpdateProductRatingCommandHandler(
    IProductRatingRepositoryV2 productRatingRepository)
    : IRequestHandler<UpdateProductRatingCommand, UpdateProductRatingResponse>
{
    public async Task<UpdateProductRatingResponse> Handle(UpdateProductRatingCommand request, CancellationToken cancellationToken)
    {
        var existing = await productRatingRepository.GetProductRatingByIdAsync(
            new GetProductRatingByIdRequest { Id = request.Request.Id }, cancellationToken);
        if (existing is null)
            return new UpdateProductRatingResponse(false);

        await productRatingRepository.UpdateProductRatingAsync(request.Request, cancellationToken);
        return new UpdateProductRatingResponse(true);
    }
}