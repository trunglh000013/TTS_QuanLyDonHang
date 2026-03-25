using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record DeleteProductRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
