using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record GetProductByCategoryRequest : PaginationRequest
{
    [Required]
    [StringLength(100)]
    public string Category { get; set; } = string.Empty;
}
