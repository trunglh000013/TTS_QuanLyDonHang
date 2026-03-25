namespace ProductTest.Application.DTOs.Response.Order;

public record GetOrderByIdResponse
{
    public OrderDto? Order { get; set; }
}
