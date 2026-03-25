namespace ProductTest.Application.DTOs.Response.Customer;

public record GetCustomerByCodeResponse
{
    public CustomerDto? Customer { get; set; }
}
