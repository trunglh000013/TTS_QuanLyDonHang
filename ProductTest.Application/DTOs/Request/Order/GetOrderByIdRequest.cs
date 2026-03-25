using System.ComponentModel.DataAnnotations;
using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record GetOrderByIdRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
