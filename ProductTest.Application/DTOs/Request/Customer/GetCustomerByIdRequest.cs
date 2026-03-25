using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Customer;

public sealed record GetCustomerByIdRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
