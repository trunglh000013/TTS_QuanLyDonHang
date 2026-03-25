using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.CustomerAbstractions;

public interface ICustomerRepositoryV2
{
    Task<Customer?> GetCustomerByIdAsync(GetCustomerByIdRequest request, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerByCodeAsync(GetCustomerByCodeRequest request, CancellationToken cancellationToken = default);
    Task<List<Customer>> GetAllCustomersAsync(GetAllCustomerRequest request, CancellationToken cancellationToken = default);
    Task CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default);
    Task UpdateCustomerAsync(UpdateCustomerRequest request, CancellationToken cancellationToken = default);
    Task DeleteCustomerAsync(DeleteCustomerRequest request, CancellationToken cancellationToken = default);
}