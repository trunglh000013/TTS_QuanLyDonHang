namespace ProductTest.Application.DTOs.Response.Cart;

/// <summary>
/// Giỏ hàng + dòng con (read-model).
/// </summary>
public sealed class CartDto
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
}
