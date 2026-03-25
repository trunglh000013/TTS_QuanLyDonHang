using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.OrderRepository;

public sealed class OrderRepositoryV2(
    ILogger<OrderRepositoryV2> logger,
    IStoreProcedureRunner spRunner)
    : IOrderRepositoryV2
{
    public async Task CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "Creating order. SP {StoredProcedure}, {@Request}",
            StoreProcedureOrderEnum.Create.ToProcedureString(),
            request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureOrderEnum.Create.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("CreateOrder completed for customer {CustomerId}", request.CustomerId);
    }

    public async Task CreateOrderByCartIdAsync(CreateOrderByCartIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating order by cart id. CartId: {CartId}", request.CartId);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureOrderEnum.CreateByCartId.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("CreateOrderByCartId completed for cart {CartId}", request.CartId);
    }

    public async Task DeleteOrderAsync(DeleteOrderRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "DeleteOrder SP {StoredProcedure}, {@Request}",
            StoreProcedureOrderEnum.Delete.ToProcedureString(),
            request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureOrderEnum.Delete.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("DeleteOrder completed for id {OrderId}", request.Id);
    }

    public async Task<List<Order>> GetAllOrdersAsync(GetAllOrderRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetAllOrders SP {StoredProcedure}, page {PageNumber}, pageSize {PageSize}",
            StoreProcedureOrderEnum.GetAll.ToProcedureString(),
            request.PageNumber,
            request.PageSize);

        var orders = await spRunner.ExecuteProcedureAsync<Order>(
            StoreProcedureOrderEnum.GetAll.ToProcedureString(),
            request,
            cancellationToken
        );
        var list = orders.ToList();
        logger.LogInformation("GetAllOrders returned {Count} rows", list.Count);
        return list;
    }

    public async Task<Order?> GetOrderByCodeAsync(GetOrderByCodeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetOrderByCode SP {StoredProcedure}, code {Code}",
            StoreProcedureOrderEnum.GetByCode.ToProcedureString(),
            request.Code);

        var orders = await spRunner.ExecuteProcedureAsync<Order>(
            StoreProcedureOrderEnum.GetByCode.ToProcedureString(),
            request,
            cancellationToken
        );
        var row = orders.FirstOrDefault();
        if (row is null)
            logger.LogWarning("GetOrderByCode found no row for code {Code}", request.Code);
        else
            logger.LogInformation("GetOrderByCode found order id {OrderId}", row.Id);
        return row;
    }

    public async Task<List<Order>> GetOrderByCustomerIdAsync(GetOrderByCustomerIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetOrderByCustomerId SP {StoredProcedure}, customerId {CustomerId}",
            StoreProcedureOrderEnum.GetByCustomerId.ToProcedureString(),
            request.CustomerId);

        var orders = await spRunner.ExecuteProcedureAsync<Order>(
            StoreProcedureOrderEnum.GetByCustomerId.ToProcedureString(),
            request,
            cancellationToken
        );
        var list = orders.ToList();
        logger.LogInformation("GetOrderByCustomerId returned {Count} rows", list.Count);
        return list;
    }

    public async Task<Order?> GetOrderByIdAsync(GetOrderByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetOrderById SP {StoredProcedure}, id {OrderId}",
            StoreProcedureOrderEnum.GetById.ToProcedureString(),
            request.Id);

        var orders = await spRunner.ExecuteProcedureAsync<Order>(
            StoreProcedureOrderEnum.GetById.ToProcedureString(),
            request,
            cancellationToken
        );
        var row = orders.FirstOrDefault();
        if (row is null)
            logger.LogWarning("GetOrderById found no row for id {OrderId}", request.Id);
        else
            logger.LogInformation("GetOrderById found code {Code}", row.Code);
        return row;
    }

    public async Task<List<OrderItem>> GetOrderDetailAsync(GetOrderDetailRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetOrderDetail SP {StoredProcedure}, orderId {OrderId}",
            StoreProcedureOrderEnum.GetDetail.ToProcedureString(),
            request.OrderId);

        var orderItems = await spRunner.ExecuteProcedureAsync<OrderItem>(
            StoreProcedureOrderEnum.GetDetail.ToProcedureString(),
            request,
            cancellationToken
        );

        var list = orderItems.ToList();
        logger.LogInformation("GetOrderDetail returned {Count} line items", list.Count);
        return list;
    }

    public async Task UpdateOrderAsync(UpdateOrderRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "UpdateOrder SP {StoredProcedure}, id {OrderId}, body {@Body}",
            StoreProcedureOrderEnum.Update.ToProcedureString(),
            request.Id,
            request.UpdateOrderBody);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureOrderEnum.Update.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("UpdateOrder completed for id {OrderId}", request.Id);
    }
}
