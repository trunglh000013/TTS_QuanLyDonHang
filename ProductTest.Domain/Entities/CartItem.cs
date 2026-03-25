namespace ProductTest.Domain.Entities;

public sealed class CartItem
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CartId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
