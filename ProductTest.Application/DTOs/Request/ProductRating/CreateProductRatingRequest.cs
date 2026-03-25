using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.ProductRating;

public sealed record CreateProductRatingRequest
{
    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;

    [Required]
    [StringLength(64)]
    public string CustomerId { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Stars { get; set; }

    [StringLength(2000)]
    public string Content { get; set; } = string.Empty;
}
