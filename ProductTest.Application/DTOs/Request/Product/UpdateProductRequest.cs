using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Product;

public sealed record UpdateProductRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdateProductBody? Body { get; set; }
}

public sealed record UpdateProductBody
{
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(100)]
    public string Category { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }

    [Range(typeof(decimal), "0", "100")]
    public decimal TaxRate { get; set; } = 10m;

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    /// <summary>
    /// <c>Suppliers.Id</c> (CHAR(10)), có thể null để bỏ gán NCC.
    /// </summary>
    [StringLength(64)]
    public string? SupplierId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime ExpiredDT { get; set; }
}
