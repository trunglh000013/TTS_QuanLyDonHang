using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;

namespace ProductTest.Application.Features.v2.ProductRatings.Commands.DeleteProductRating;

public sealed record DeleteProductRatingCommand(DeleteProductRatingRequest Request) : IRequest<DeleteProductRatingResponse>;

public sealed class DeleteProductRatingCommandHandler(
    IProductRatingRepositoryV2 productRatingRepository)
    : IRequestHandler<DeleteProductRatingCommand, DeleteProductRatingResponse>
{
    public async Task<DeleteProductRatingResponse> Handle(DeleteProductRatingCommand request, CancellationToken cancellationToken)
    {
        var existing = await productRatingRepository.GetProductRatingByIdAsync(new GetProductRatingByIdRequest { Id = request.Request.Id }, cancellationToken);
        if (existing is null)
            return new DeleteProductRatingResponse(false);

        await productRatingRepository.DeleteProductRatingAsync(request.Request, cancellationToken);
        return new DeleteProductRatingResponse(true);
    }
}