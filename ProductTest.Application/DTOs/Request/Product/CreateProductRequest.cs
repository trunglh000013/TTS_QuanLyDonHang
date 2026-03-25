using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record CreateProductRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Category { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }

    /// <summary>Thuế suất VAT % (0 = miễn thuế).</summary>
    [Range(typeof(decimal), "0", "100")]
    public decimal TaxRate { get; set; } = 10m;

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [StringLength(64)]
    public string? SupplierId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime ExpiredDT { get; set; }
}
