using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.SupplierAbstractions;

public interface ISupplierRepositoryV2
{
    Task<List<Supplier>> GetSuppliersByProductIdAsync(GetSupplierByProductIdRequest request, CancellationToken cancellationToken = default);
    Task<Supplier?> GetSupplierByIdAsync(GetSupplierByIdRequest request, CancellationToken cancellationToken = default);
    Task<Supplier?> GetSupplierByCodeAsync(GetSupplierByCodeRequest request, CancellationToken cancellationToken = default);
    Task<List<Supplier>> GetAllSuppliersAsync(GetAllSupplierRequest request, CancellationToken cancellationToken = default);
    Task CreateSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken = default);
    Task UpdateSupplierAsync(UpdateSupplierRequest request, CancellationToken cancellationToken = default);
    Task DeleteSupplierAsync(DeleteSupplierRequest request, CancellationToken cancellationToken = default);
}