namespace ProductTest.Application.DTOs.Response.Product;

public sealed record ProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public int Stock { get; set; }
    public string? SupplierId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime ExpiredDT { get; set; }
}
