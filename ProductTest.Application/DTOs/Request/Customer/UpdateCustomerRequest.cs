using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Customer;

public sealed record UpdateCustomerRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public UpdateCustomerBody? Body { get; set; }
}

public sealed record UpdateCustomerBody
{
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

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
