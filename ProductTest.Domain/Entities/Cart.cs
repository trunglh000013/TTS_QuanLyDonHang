namespace ProductTest.Domain.Entities;

public sealed class Cart
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Customer Customer { get; set; } = null!;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
