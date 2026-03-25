namespace ProductTest.Application.DTOs.Response.Product;

public record GetProductByIdResponse
{
    public ProductDto? Product { get; set; }
}