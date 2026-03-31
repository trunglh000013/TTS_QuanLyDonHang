
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.CustomerRepository;

public sealed class CustomerRepositoryV2(
    ILogger<CustomerRepositoryV2> logger,
    IStoreProcedureRunner spRunner)
    : ICustomerRepositoryV2
{
    public async Task CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CustomerCreate SP {StoredProcedure}, payload {@Request}",
            StoreProcedureCustomerEnum.CustomerCreate.ToProcedureString(),
            request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCustomerEnum.CustomerCreate.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("CustomerCreate completed for code {Code}", request.Code);
    }

    public async Task DeleteCustomerAsync(DeleteCustomerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CustomerDelete SP {StoredProcedure}, id {CustomerId}",
            StoreProcedureCustomerEnum.CustomerDelete.ToProcedureString(),
            request.Id);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCustomerEnum.CustomerDelete.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("CustomerDelete completed for id {CustomerId}", request.Id);
    }

    public async Task<List<Customer>> GetAllCustomersAsync(GetAllCustomerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CustomerGetAll SP {StoredProcedure}, page {PageNumber}, pageSize {PageSize}",
            StoreProcedureCustomerEnum.CustomerGetAll.ToProcedureString(),
            request.PageNumber,
            request.PageSize);

        var customers = await spRunner.ExecuteProcedureAsync<Customer>(
            StoreProcedureCustomerEnum.CustomerGetAll.ToProcedureString(),
            request,
            cancellationToken
        );
        var list = customers.ToList();
        logger.LogInformation("CustomerGetAll returned {Count} rows", list.Count);
        return list;
    }

    public async Task<Customer?> GetCustomerByCodeAsync(GetCustomerByCodeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CustomerGetByCode SP {StoredProcedure}, code {Code}",
            StoreProcedureCustomerEnum.CustomerGetByCode.ToProcedureString(),
            request.Code);

        var customers = await spRunner.ExecuteProcedureAsync<Customer>(
            StoreProcedureCustomerEnum.CustomerGetByCode.ToProcedureString(),
            request,
            cancellationToken
        );
        var row = customers.FirstOrDefault();
        if (row is null)
            logger.LogWarning("CustomerGetByCode found no row for code {Code}", request.Code);
        else
            logger.LogInformation("CustomerGetByCode found id {CustomerId}", row.Id);
        return row;
    }

    public async Task<Customer?> GetCustomerByIdAsync(GetCustomerByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CustomerGetById SP {StoredProcedure}, id {CustomerId}",
            StoreProcedureCustomerEnum.CustomerGetById.ToProcedureString(),
            request.Id);

        var customers = await spRunner.ExecuteProcedureAsync<Customer>(
            StoreProcedureCustomerEnum.CustomerGetById.ToProcedureString(),
            request,
            cancellationToken
        );
        var row = customers.FirstOrDefault();
        if (row is null)
            logger.LogWarning("CustomerGetById found no row for id {CustomerId}", request.Id);
        else
            logger.LogInformation("CustomerGetById found code {Code}", row.Code);
        return row;
    }

    public async Task UpdateCustomerAsync(UpdateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CustomerUpdate SP {StoredProcedure}, id {CustomerId}, body {@Body}",
            StoreProcedureCustomerEnum.CustomerUpdate.ToProcedureString(),
            request.Id,
            request.Body);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureCustomerEnum.CustomerUpdate.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("CustomerUpdate completed for id {CustomerId}", request.Id);
    }
}
