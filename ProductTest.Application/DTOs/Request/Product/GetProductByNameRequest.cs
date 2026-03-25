using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record GetProductByNameRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}
