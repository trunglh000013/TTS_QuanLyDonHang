namespace ProductTest.Domain.Entities;

public sealed class ProductRating
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public int Stars { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Product Product { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
