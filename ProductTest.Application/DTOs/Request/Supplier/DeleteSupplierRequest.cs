using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Supplier;

public sealed record DeleteSupplierRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
