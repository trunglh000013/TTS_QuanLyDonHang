using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.SupplierRepository;

public sealed class SupplierRepositoryV2(
    ILogger<SupplierRepositoryV2> logger,
    IStoreProcedureRunner spRunner)
    : ISupplierRepositoryV2
{
    public async Task CreateSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "CreateSupplier SP {StoredProcedure}, payload {@Request}",
            StoreProcedureSupplierEnum.CreateSupplier.ToProcedureString(),
            request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureSupplierEnum.CreateSupplier.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("CreateSupplier completed for name {SupplierName}", request.Name);
    }

    public async Task DeleteSupplierAsync(DeleteSupplierRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "DeleteSupplier SP {StoredProcedure}, payload {@Request}",
            StoreProcedureSupplierEnum.DeleteSupplier.ToProcedureString(),
            request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureSupplierEnum.DeleteSupplier.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("DeleteSupplier completed for id {SupplierId}", request.Id);
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync(GetAllSupplierRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "GetAllSuppliers SP {StoredProcedure}, page {PageNumber}, pageSize {PageSize}",
            StoreProcedureSupplierEnum.GetAllSuppliers.ToProcedureString(),
            request.PageNumber,
            request.PageSize);

        var suppliers = await spRunner.ExecuteProcedureAsync<Supplier>(
            StoreProcedureSupplierEnum.GetAllSuppliers.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("GetAllSuppliers returned {Count} rows", suppliers.Count);
        return suppliers;
    }

    public async Task<Supplier?> GetSupplierByCodeAsync(GetSupplierByCodeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetSupplierByCode SP {StoredProcedure}, code {Code}",
            StoreProcedureSupplierEnum.GetSupplierByCode.ToProcedureString(),
            request.Code);

        var suppliers = await spRunner.ExecuteProcedureAsync<Supplier>(
            StoreProcedureSupplierEnum.GetSupplierByCode.ToProcedureString(),
            request,
            cancellationToken
        );

        var row = suppliers.FirstOrDefault();
        if (row is null)
            logger.LogWarning("GetSupplierByCode found no row for code {Code}", request.Code);
        else
            logger.LogInformation("GetSupplierByCode found supplier id {SupplierId}", row.Id);

        return row;
    }

    public async Task<Supplier?> GetSupplierByIdAsync(GetSupplierByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetSupplierById SP {StoredProcedure}, id {SupplierId}",
            StoreProcedureSupplierEnum.GetSupplierById.ToProcedureString(),
            request.Id);

        var suppliers = await spRunner.ExecuteProcedureAsync<Supplier>(
            StoreProcedureSupplierEnum.GetSupplierById.ToProcedureString(),
            request,
            cancellationToken
        );

        var row = suppliers.FirstOrDefault();
        if (row is null)
            logger.LogWarning("GetSupplierById found no row for id {SupplierId}", request.Id);
        else
            logger.LogInformation("GetSupplierById found supplier code {Code}", row.Code);

        return row;
    }

    public async Task<List<Supplier>> GetSuppliersByProductIdAsync(GetSupplierByProductIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "GetSuppliersByProductId SP {StoredProcedure}, productId {ProductId}",
            StoreProcedureSupplierEnum.GetSupplierByProductId.ToProcedureString(),
            request.ProductId);

        var suppliers = await spRunner.ExecuteProcedureAsync<Supplier>(
            StoreProcedureSupplierEnum.GetSupplierByProductId.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("GetSuppliersByProductId returned {Count} rows", suppliers.Count);
        return suppliers;
    }

    public async Task UpdateSupplierAsync(UpdateSupplierRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "UpdateSupplier SP {StoredProcedure}, id {SupplierId}, payload {@Body}",
            StoreProcedureSupplierEnum.UpdateSupplier.ToProcedureString(),
            request.Id,
            request.UpdateSupplierBody);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureSupplierEnum.UpdateSupplier.ToProcedureString(),
            request,
            cancellationToken
        );

        logger.LogInformation("UpdateSupplier completed for id {SupplierId}", request.Id);
    }
}

