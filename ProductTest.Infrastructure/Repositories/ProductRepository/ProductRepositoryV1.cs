using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductTest.Application.DTOs;
using ProductTest.Infrastructure.Persistence;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs.Response.Product;
using ProductTest.Domain.Entities;

namespace ProductTest.Infrastructure.Repositories.ProductRepository;

public sealed class ProductRepositoryV1(
    ProductDbContext dbContext,
    ILogger<ProductRepositoryV1> logger,
    IMapper mapper) : IProductRepositoryV1
{
    public async Task<Product?> GetByIdAsync(GetProductByIdRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Fetching product by id {ProductId}", request.Id);

        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

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
        logger.LogInformation("Fetching product by name {ProductName}", request.Name);

        var trimmedName = request.Name.Trim();

        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product =>
                EF.Functions.Like(product.Name, trimmedName), cancellationToken);

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
        logger.LogInformation("Fetching product by category {ProductCategory}", request.Category);

        var trimmedCategory = request.Category.Trim();

        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product =>
                EF.Functions.Like(product.Category, trimmedCategory), cancellationToken);

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
            "Fetching products page {PageNumber} with page size {PageSize}",
            request.PageNumber,
            request.PageSize);

        var result =
            await dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "Fetched {ItemCount} products for page {PageNumber}",
            result.Count,
            request.PageNumber);

        return result;
    }

    public async Task<List<Product>> SearchAsync(
        SearchProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedTerm = request.SearchTerm.Trim();

        logger.LogInformation(
            "Searching products with term {SearchTerm}, page {PageNumber}, page size {PageSize}",
            normalizedTerm,
            request.PageNumber,
            request.PageSize);

        var result = await dbContext.Products
            .AsNoTracking()
            .Where(product =>
                product.Name.Contains(normalizedTerm) ||
                product.Description.Contains(normalizedTerm) ||
                product.Category.Contains(normalizedTerm))
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "Search for term {SearchTerm} returned {ItemCount} products",
            normalizedTerm,
            result.Count);

        return result;
    }

    public async Task<List<Product>> FilterAsync(
        FilterProductRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Filtering products: category {Category}, min {MinPrice}, max {MaxPrice}, isActive {IsActive}",
            request.Category,
            request.MinPrice,
            request.MaxPrice,
            request.IsActive);

        var query = dbContext.Products.AsNoTracking().AsQueryable();

        if (request.Category is not null)
        {
            query = query.Where(product => product.Category == request.Category);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(product => product.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(product => product.Price <= request.MaxPrice.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(product => product.IsActive == request.IsActive.Value);
        }

        var result = await query
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "Filter returned {ItemCount} products",
            result.Count);

        return result;
    }

    public async Task AddAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Creating product {ProductName} in category {Category} with price {Price}",
            request.Name,
            request.Category,
            request.Price);

        await dbContext.Products.AddAsync(mapper.Map<Domain.Entities.Product>(request), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created product {ProductName} successfully", request.Name);
    }

    public async Task UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        // Validate the request and its Body.
        if (request.Body == null)
        {
            throw new ArgumentException("UpdateProductRequest.Body cannot be null", nameof(request));
        }

        logger.LogInformation(
            "Updating product {ProductName} in category {Category} with price {Price} (ID: {ProductId})",
            request.Body.Name,
            request.Body.Category,
            request.Body.Price,
            request.Id);

        // Fetch existing product.
        var existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (existingProduct is null)
        {
            logger.LogWarning("Product {ProductId} was not found", request.Id);
            throw new InvalidOperationException($"Product with ID {request.Id} was not found.");
        }

        // Update fields.
        existingProduct.Name = request.Body.Name;
        existingProduct.Description = request.Body.Description;
        existingProduct.Category = request.Body.Category;
        existingProduct.Price = request.Body.Price;
        existingProduct.Stock = request.Body.Stock;
        existingProduct.IsActive = request.Body.IsActive;

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product {ProductName} successfully", request.Body.Name);
    }

    public async Task DeleteAsync(DeleteProductRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting product {ProductId}", request.Id);

        var existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (existingProduct is null)
        {
            logger.LogWarning("Product {ProductId} was not found", request.Id);
            throw new InvalidOperationException($"Product with ID {request.Id} was not found.");
        }

        // dbContext.Products.Remove(existingProduct);

        existingProduct.IsActive = false;
        existingProduct.UpdatedAt = DateTime.UtcNow;

        dbContext.Products.Update(existingProduct);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Product {ProductId} soft-deleted (inactive) successfully", request.Id);
    }
}
