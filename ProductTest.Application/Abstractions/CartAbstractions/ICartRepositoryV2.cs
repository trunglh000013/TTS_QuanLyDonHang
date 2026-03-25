using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.CartAbstractions;

public interface ICartRepositoryV2
{
    Task<List<CartItem>> GetAllIteamCartByCustomerIdAsync(GetAllItemCartByCustomerIdRequest request, CancellationToken cancellationToken = default);
    Task CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken = default);
    Task DeleteCartAsync(DeleteCartRequest request, CancellationToken cancellationToken = default);
    Task AddItemCartAsync(AddItemCartRequest request, CancellationToken cancellationToken = default);
    Task DeleteItemCartAsync(DeleteItemCartRequest request, CancellationToken cancellationToken = default);
}
