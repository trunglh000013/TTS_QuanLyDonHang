using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record SearchProductRequest : PaginationRequest
{
    [Required]
    [StringLength(500)]
    public string SearchTerm { get; set; } = string.Empty;
}
