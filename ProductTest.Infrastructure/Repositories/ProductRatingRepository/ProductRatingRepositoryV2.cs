using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.ProductRatingRepository;

public sealed class ProductRatingRepositoryV2(
    ILogger<ProductRatingRepositoryV2> logger,
    IStoreProcedureRunner spRunner)
    : IProductRatingRepositoryV2
{
    public async Task CreateProductRatingAsync(CreateProductRatingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "ProductRating Create SP {StoredProcedure}, {@Request}",
            StoreProcedureProductRatingEnum.Create.ToProcedureString(),
            request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureProductRatingEnum.Create.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation(
            "ProductRating Create completed for product {ProductId}, customer {CustomerId}",
            request.ProductId,
            request.CustomerId);
    }

    public async Task DeleteProductRatingAsync(DeleteProductRatingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "ProductRating Delete SP {StoredProcedure}, id {RatingId}",
            StoreProcedureProductRatingEnum.Delete.ToProcedureString(),
            request.Id);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureProductRatingEnum.Delete.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("ProductRating Delete completed for id {RatingId}", request.Id);
    }

    public async Task<List<ProductRating>> GetAllProductRatingsAsync(GetAllProductRatingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        const string sp = "dbo.usp_ProductRatingGetAll";
        logger.LogInformation("ProductRating GetAll SP {StoredProcedure}, {@Request}", sp, request);

        var ratings = await spRunner.ExecuteProcedureAsync<ProductRating>(
            sp,
            request,
            cancellationToken
        );
        var list = ratings.ToList();
        logger.LogInformation("ProductRating GetAll returned {Count} rows", list.Count);
        return list;
    }

    public async Task<ProductRating?> GetProductRatingByCodeAsync(GetProductRatingByCodeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        const string sp = "dbo.usp_ProductRatingGetByCode";
        logger.LogInformation("ProductRating GetByCode SP {StoredProcedure}, code {Code}", sp, request.Code);

        var ratings = await spRunner.ExecuteProcedureAsync<ProductRating>(
            sp,
            request,
            cancellationToken
        );
        var row = ratings.FirstOrDefault();
        if (row is null)
            logger.LogWarning("ProductRating GetByCode found no row for code {Code}", request.Code);
        else
            logger.LogInformation("ProductRating GetByCode found id {RatingId}", row.Id);
        return row;
    }

    public async Task<ProductRating?> GetProductRatingByIdAsync(GetProductRatingByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var sp = StoreProcedureProductRatingEnum.GetById.ToProcedureString();
        logger.LogInformation("ProductRating GetById SP {StoredProcedure}, id {RatingId}", sp, request.Id);

        var ratings = await spRunner.ExecuteProcedureAsync<ProductRating>(
            sp,
            request,
            cancellationToken
        );
        var row = ratings.FirstOrDefault();
        if (row is null)
            logger.LogWarning("ProductRating GetById found no row for id {RatingId}", request.Id);
        else
            logger.LogInformation("ProductRating GetById found code {Code}", row.Code);
        return row;
    }

    public async Task<List<ProductRating>> GetProductRatingsByProductIdAsync(GetProductRatingByProductIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var sp = StoreProcedureProductRatingEnum.GetByProductId.ToProcedureString();
        logger.LogInformation("ProductRating GetByProductId SP {StoredProcedure}, productId {ProductId}", sp, request.ProductId);

        var ratings = await spRunner.ExecuteProcedureAsync<ProductRating>(
            sp,
            request,
            cancellationToken
        );
        var list = ratings.ToList();
        logger.LogInformation("ProductRating GetByProductId returned {Count} rows", list.Count);
        return list;
    }

    public async Task UpdateProductRatingAsync(UpdateProductRatingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "ProductRating Update SP {StoredProcedure}, id {RatingId}, body {@Body}",
            StoreProcedureProductRatingEnum.Update.ToProcedureString(),
            request.Id,
            request.Body);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureProductRatingEnum.Update.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("ProductRating Update completed for id {RatingId}", request.Id);
    }
}

