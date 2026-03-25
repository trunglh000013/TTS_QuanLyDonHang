using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.ProductRatingAbstractions;

public interface IProductRatingRepositoryV2
{
    Task<List<ProductRating>> GetProductRatingsByProductIdAsync(GetProductRatingByProductIdRequest request, CancellationToken cancellationToken = default);
    Task<ProductRating?> GetProductRatingByIdAsync(GetProductRatingByIdRequest request, CancellationToken cancellationToken = default);
    Task<ProductRating?> GetProductRatingByCodeAsync(GetProductRatingByCodeRequest request, CancellationToken cancellationToken = default);
    Task<List<ProductRating>> GetAllProductRatingsAsync(GetAllProductRatingRequest request, CancellationToken cancellationToken = default);
    Task CreateProductRatingAsync(CreateProductRatingRequest request, CancellationToken cancellationToken = default);
    Task UpdateProductRatingAsync(UpdateProductRatingRequest request, CancellationToken cancellationToken = default);
    Task DeleteProductRatingAsync(DeleteProductRatingRequest request, CancellationToken cancellationToken = default);
}