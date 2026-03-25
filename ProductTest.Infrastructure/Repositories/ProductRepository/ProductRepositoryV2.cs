using Microsoft.Extensions.Logging;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Domain.Entities;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Common.Mapping;
using AutoMapper;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.ProductRepository;

public sealed class ProductRepositoryV2(
    ILogger<ProductRepositoryV2> logger,
    IStoreProcedureRunner _spRunner,
    IMapper mapper
) : IProductRepositoryV2
{
    public async Task<Product?> GetByIdAsync(GetProductByIdRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Fetching product by id {ProductId} via stored procedure {StoredProcedure}",
            request.Id,
            StoreProcedureProductEnum.GetById.ToProcedureString());

        var products = await _spRunner.ExecuteProcedureAsync<Product>(
            StoreProcedureProductEnum.GetById.ToProcedureString(),
            request,
            cancellationToken);
        var product = products.FirstOrDefault();

        if (product is null)
        {
            logger.LogWarning("Product {ProductId} was not found", request.Id);
            return null;
        }

        logger.LogInformation("Fetched product {ProductId} successfully", request.Id);
        return product;
    }

    public async Task<Product?> GetByNameAsync(GetProductByNameRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Fetching product by name {ProductName} via stored procedure {StoredProcedure}",
            request.Name,
            StoreProcedureProductEnum.GetByName.ToProcedureString());

        var products = await _spRunner.ExecuteProcedureAsync<Product>(
            StoreProcedureProductEnum.GetByName.ToProcedureString(),
            request,
            cancellationToken);
        var product = products.FirstOrDefault();

        if (product is null)
        {
            logger.LogWarning("Product {ProductName} was not found", request.Name);
            return null;
        }

        logger.LogInformation("Fetched product {ProductName} successfully", request.Name);
        return product;
    }

    public async Task<Product?> GetByCategoryAsync(GetProductByCategoryRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Fetching product by category {ProductCategory} via stored procedure {StoredProcedure}",
            request.Category,
            StoreProcedureProductEnum.GetByCategory.ToProcedureString());

        var products = await _spRunner.ExecuteProcedureAsync<Product>(
            StoreProcedureProductEnum.GetByCategory.ToProcedureString(),
            request,
            cancellationToken);
        var product = products.FirstOrDefault();

        if (product is null)
        {
            logger.LogWarning("Product {ProductCategory} was not found", request.Category);
            return null;
        }

        logger.LogInformation("Fetched product {ProductCategory} successfully", request.Category);
        return product;
    }

    public async Task<List<Product>> GetAllAsync(
        GetAllProductRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Fetching all products (paged) via stored procedure {StoredProcedure}, page {PageNumber}, page size {PageSize}",
            StoreProcedureProductEnum.Filter.ToProcedureString(),
            request.PageNumber,
            request.PageSize);

        var products = await _spRunner.ExecuteProcedureAsync<Product>(
            StoreProcedureProductEnum.Filter.ToProcedureString(),
            request,
            cancellationToken);
        logger.LogInformation("GetAll returned {Count} products", products.Count);
        return products;
    }

    public async Task<List<Product>> SearchAsync(
        SearchProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedTerm = request.SearchTerm.Trim();

        logger.LogInformation(
            "Searching products with term {SearchTerm}, using stored procedure {StoredProcedure} (paged in SQL)",
            normalizedTerm,
            StoreProcedureProductEnum.Search.ToProcedureString());

        var products = await _spRunner.ExecuteProcedureAsync<Product>(
            StoreProcedureProductEnum.Search.ToProcedureString(),
            request,
            cancellationToken);

        logger.LogInformation("Search returned {Count} products for term {SearchTerm}", products.Count, normalizedTerm);
        return products;
    }

    public async Task<List<Product>> FilterAsync(
        FilterProductRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Filtering products with category {Category}, min price {MinPrice}, max price {MaxPrice}, is active {IsActive}, page {PageNumber}, page size {PageSize} using stored procedure {StoredProcedure}",
            request.Category,
            request.MinPrice,
            request.MaxPrice,
            request.IsActive,
            request.PageNumber,
            request.PageSize,
            StoreProcedureProductEnum.Filter.ToProcedureString());

        var products = await _spRunner.ExecuteProcedureAsync<Product>(
            StoreProcedureProductEnum.Filter.ToProcedureString(),
            request,
            cancellationToken);

        logger.LogInformation("Filter returned {Count} products", products.Count);
        return products;
    }

    public async Task AddAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Creating product {ProductName} in category {Category} with price {Price} using stored procedure {StoredProcedure}",
            request.Name,
            request.Category,
            request.Price,
            StoreProcedureProductEnum.Create.ToProcedureString());

        await _spRunner.ExecuteNonQueryAsync(
            StoreProcedureProductEnum.Create.ToProcedureString(),
            request,
            cancellationToken);

        logger.LogInformation("Created product {ProductName} successfully", request.Name);
    }

    public async Task UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentException("UpdateProductRequest.Body cannot be null", nameof(request));

        logger.LogInformation("Updating product {ProductName} using stored procedure {StoredProcedure}", request.Body?.Name, StoreProcedureProductEnum.Update.ToProcedureString());

        var product = mapper.Map<Product>(request.Body);
        product.Id = request.Id;

        await _spRunner.ExecuteNonQueryAsync(
            StoreProcedureProductEnum.Update.ToProcedureString(),
            product,
            cancellationToken);

        logger.LogInformation("Updated product {ProductName} successfully", request.Body?.Name);
    }

    public async Task DeleteAsync(DeleteProductRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting product {ProductId} using stored procedure {StoredProcedure}", request.Id, StoreProcedureProductEnum.Delete.ToProcedureString());

        await _spRunner.ExecuteNonQueryAsync(
            StoreProcedureProductEnum.Delete.ToProcedureString(),
            request,
            cancellationToken);

        logger.LogInformation("Soft deleted product {ProductId} via {StoredProcedure}", request.Id, StoreProcedureProductEnum.Delete.ToProcedureString());
    }
}
