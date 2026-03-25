using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.ProductRating;

public sealed record UpdateProductRatingRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdateProductRatingBody? Body { get; set; }
}

public sealed record UpdateProductRatingBody
{
    [Range(1, 5)]
    public int Stars { get; set; }

    [StringLength(2000)]
    public string Content { get; set; } = string.Empty;
}
