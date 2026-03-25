using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Customer;

public sealed record GetCustomerByCodeRequest
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;
}
