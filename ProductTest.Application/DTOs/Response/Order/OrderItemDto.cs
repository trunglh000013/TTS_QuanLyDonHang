namespace ProductTest.Application.DTOs.Response.Order;

/// <summary>
/// Chi tiết dòng trong đơn (<c>OrderItems</c>).
/// </summary>
public sealed class OrderItemDto
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public decimal LineSubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
