using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Supplier;

public sealed record CreateSupplierRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;

    [StringLength(500)]
    public string Address { get; set; } = string.Empty;

    [StringLength(32)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(256)]
    public string Email { get; set; } = string.Empty;
}
