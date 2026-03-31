using System.Data;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Domain.Entities;
namespace ProductTest.Application.Abstractions.ProductAbstractions;

public interface IProductRepositoryV2
{
    Task<Product?> GetByIdAsync(GetProductByIdRequest request, CancellationToken cancellationToken = default);
    Task<Product?> GetByNameAsync(GetProductByNameRequest request, CancellationToken cancellationToken = default);
    Task<Product?> GetByCategoryAsync(GetProductByCategoryRequest request, CancellationToken cancellationToken = default);

    Task<List<Product>> GetAllAsync(GetAllProductRequest request, CancellationToken cancellationToken = default);

    Task<DataSet> GetAllToDataSetAsync(GetAllProductRequest request, CancellationToken cancellationToken = default);

    Task<List<Product>> SearchAsync(SearchProductRequest request, CancellationToken cancellationToken = default);

    Task<List<Product>> FilterAsync(FilterProductRequest request, CancellationToken cancellationToken = default);

    Task AddAsync(CreateProductRequest request, CancellationToken cancellationToken = default);

    Task UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(DeleteProductRequest request, CancellationToken cancellationToken = default);
}
