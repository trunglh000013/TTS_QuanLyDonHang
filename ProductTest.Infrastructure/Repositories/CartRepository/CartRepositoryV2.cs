using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.CartRepository;

public sealed class CartRepositoryV2(
    ILogger<CartRepositoryV2> logger,
    IStoreProcedureRunner spRunner)
    : ICartRepositoryV2
{
    public async Task AddItemCartAsync(AddItemCartRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Adding item to cart. Request: {@Request}", request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCartEnum.CartAddItem.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("Item successfully added to cart.");
    }

    public async Task CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating new cart. Request: {@Request}", request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCartEnum.CartCreate.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("Cart created successfully.");
    }

    public async Task DeleteCartAsync(DeleteCartRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Deleting cart. Request: {@Request}", request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCartEnum.CartDelete.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("Cart deleted successfully.");
    }

    public async Task DeleteItemCartAsync(DeleteItemCartRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Deleting item from cart. Request: {@Request}", request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCartEnum.CartDeleteItem.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("Item deleted from cart successfully.");
    }

    public async Task<List<CartItem>> GetAllIteamCartByCustomerIdAsync(GetAllItemCartByCustomerIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Fetching all cart items by customer id. Request: {@Request}", request);

        var carts = await spRunner.ExecuteProcedureAsync<CartItem>(
            StoreProcedureCartEnum.CartGetAllItemByCustomerId.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("Retrieved {Count} cart items for customer.", carts?.Count ?? 0);

        return carts ?? new List<CartItem>();
    }
}
