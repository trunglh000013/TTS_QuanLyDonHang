namespace ProductTest.Application.DTOs.Response.Order;

public record GetOrderByCodeResponse
{
    public OrderDto? Order { get; set; }
}
