namespace ProductTest.Application.DTOs.Response.ProductRating;

public sealed record GetProductRatingByCodeResponse
{
    public ProductRatingDto? ProductRating { get; set; }
}