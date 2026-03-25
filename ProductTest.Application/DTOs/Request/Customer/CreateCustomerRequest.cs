using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Customer;

public sealed record CreateCustomerRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [StringLength(32)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(500)]
    public string Address { get; set; } = string.Empty;

    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    [StringLength(100)]
    public string State { get; set; } = string.Empty;

    [StringLength(20)]
    public string ZipCode { get; set; } = string.Empty;

    [StringLength(100)]
    public string Country { get; set; } = string.Empty;
}
