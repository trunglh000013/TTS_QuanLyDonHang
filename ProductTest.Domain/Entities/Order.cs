namespace ProductTest.Domain.Entities;

/// <summary>
/// Đơn hàng (bảng <c>Orders</c> trong DB).
/// </summary>
public sealed class Order
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string? CartId { get; set; }
    public string Status { get; set; } = "Placed";
    /// <summary>Tổng tiền hàng chưa VAT.</summary>
    public decimal SubTotal { get; set; }
    /// <summary>Tổng tiền thuế (hóa đơn).</summary>
    public decimal TaxTotal { get; set; }
    /// <summary>Tổng thanh toán gồm thuế.</summary>
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Customer Customer { get; set; } = null!;
    public Cart? Cart { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
