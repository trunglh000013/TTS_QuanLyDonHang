using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record GetOrderByCustomerIdRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string CustomerId { get; set; } = string.Empty;
}
