namespace ProductTest.Application.DTOs.Response.Customer;

public record GetCustomerByIdResponse
{
    public CustomerDto? Customer { get; set; }
}
