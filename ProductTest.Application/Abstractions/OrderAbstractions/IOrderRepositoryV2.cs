using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.OrderAbstractions;

public interface IOrderRepositoryV2
{
    Task<List<Order>> GetOrderByCustomerIdAsync(GetOrderByCustomerIdRequest request, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAsync(GetOrderByIdRequest request, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByCodeAsync(GetOrderByCodeRequest request, CancellationToken cancellationToken = default);
    Task<List<Order>> GetAllOrdersAsync(GetAllOrderRequest request, CancellationToken cancellationToken = default);
    Task<List<OrderItem>> GetOrderDetailAsync(GetOrderDetailRequest request, CancellationToken cancellationToken = default);
    Task CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
    Task CreateOrderByCartIdAsync(CreateOrderByCartIdRequest request, CancellationToken cancellationToken = default);
    Task UpdateOrderAsync(UpdateOrderRequest request, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(DeleteOrderRequest request, CancellationToken cancellationToken = default);
}