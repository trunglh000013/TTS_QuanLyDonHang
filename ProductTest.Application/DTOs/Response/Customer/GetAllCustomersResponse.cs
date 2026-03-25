using ProductTest.Application.DTOs;

namespace ProductTest.Application.DTOs.Response.Customer;

public record GetAllCustomersResponse : PaginationResponse<CustomerDto>;
