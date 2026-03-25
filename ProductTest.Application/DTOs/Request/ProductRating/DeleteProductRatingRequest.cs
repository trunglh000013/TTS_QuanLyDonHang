using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.ProductRating;

public sealed record DeleteProductRatingRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
