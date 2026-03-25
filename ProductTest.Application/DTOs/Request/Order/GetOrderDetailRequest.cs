using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record GetOrderDetailRequest : PaginationRequest
{
    [Required]
    [StringLength(64)]
    public string OrderId { get; set; } = string.Empty;
}
