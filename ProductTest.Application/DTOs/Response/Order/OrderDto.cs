namespace ProductTest.Application.DTOs.Response.Order;

/// <summary>
/// Đơn hàng + các dòng (read-model).
/// </summary>
public sealed class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string? CartId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}
