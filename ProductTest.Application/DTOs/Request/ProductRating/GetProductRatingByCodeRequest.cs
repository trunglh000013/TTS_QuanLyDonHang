using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.ProductRating;

public sealed record GetProductRatingByCodeRequest
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;
}
