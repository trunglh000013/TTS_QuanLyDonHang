namespace ProductTest.Application.DTOs.Response.ProductRating;

public sealed record GetProductRatingByIdResponse
{
    public ProductRatingDto ProductRating { get; init; } = new();
}
