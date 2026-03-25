using MediatR;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;

namespace ProductTest.Application.Features.v2.ProductRatings.Commands.CreateProductRating;

public sealed record CreateProductRatingCommand(CreateProductRatingRequest Request) : IRequest<CreateProductRatingResponse>;

public sealed class CreateProductRatingCommandHandler(
    IProductRatingRepositoryV2 productRatingRepository)
    : IRequestHandler<CreateProductRatingCommand, CreateProductRatingResponse>
{
    public async Task<CreateProductRatingResponse> Handle(CreateProductRatingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await productRatingRepository.CreateProductRatingAsync(request.Request, cancellationToken);
            return new CreateProductRatingResponse(true);
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}