using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Supplier;

public sealed record UpdateSupplierRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdateSupplierBody? UpdateSupplierBody { get; set; }
}

public sealed record UpdateSupplierBody
{
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Address { get; set; } = string.Empty;

    [StringLength(32)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(256)]
    public string Email { get; set; } = string.Empty;
}
