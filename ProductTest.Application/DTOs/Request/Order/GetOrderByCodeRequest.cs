using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record GetOrderByCodeRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; } = string.Empty;
}
