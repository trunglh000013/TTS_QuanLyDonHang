namespace ProductTest.Domain.Entities;

public sealed class OrderItem
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    /// <summary>Thuế suất % snapshot theo sản phẩm tại thời điểm đặt hàng.</summary>
    public decimal TaxRate { get; set; }
    public decimal LineSubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
