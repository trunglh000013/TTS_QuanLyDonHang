using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record DeleteOrderRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;
}
