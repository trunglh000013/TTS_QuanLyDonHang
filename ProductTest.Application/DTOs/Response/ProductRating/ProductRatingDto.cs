namespace ProductTest.Application.DTOs.Response.ProductRating;

public sealed record ProductRatingDto
{
    public string Id { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string ProductId { get; init; } = string.Empty;
    public string CustomerId { get; init; } = string.Empty;
    public int Stars { get; init; }
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public bool IsActive { get; init; }
}
