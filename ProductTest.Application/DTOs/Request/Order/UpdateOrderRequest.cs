using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record UpdateOrderRequest
{
    [Required]
    [StringLength(64)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(64)]
    public string CustomerId { get; set; } = string.Empty;

    [StringLength(200)]
    public string Status { get; set; } = "Placed";

    [Required]
    public UpdateOrderBody? UpdateOrderBody { get; set; }
}

public sealed record UpdateOrderBody
{
    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
