using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.ProductRating;

public sealed record GetProductRatingByProductIdRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;
}
