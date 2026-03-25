namespace ProductTest.Application.DTOs.Response.Order;

public sealed class PlaceOrderResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? OrderId { get; set; }
}
