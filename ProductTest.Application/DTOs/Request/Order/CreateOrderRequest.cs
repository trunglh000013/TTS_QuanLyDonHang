using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Order;

public sealed record CreateOrderRequest
{
    [Required]
    [StringLength(64)]
    public string CustomerId { get; set; } = string.Empty;

    [Required]
    public List<CreateOrderBody>? CreateOrderBody { get; set; }
}

public sealed record CreateOrderBody
{
    [Required]
    [StringLength(64)]
    public string ProductId { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
