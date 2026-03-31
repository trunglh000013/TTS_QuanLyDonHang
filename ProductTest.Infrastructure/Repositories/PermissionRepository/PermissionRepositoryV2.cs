using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.PermissionRepository;

public sealed class PermissionRepositoryV2(
    ILogger<PermissionRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IPermissionRepositoryV2
{
    public async Task<List<Permission>> GetAllPermissionsAsync(GetAllPermissionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var items = await spRunner.ExecuteProcedureAsync<Permission>(
            StoreProcedurePermissionEnum.PermissionGetAll.ToProcedureString(),
            request,
            cancellationToken);

        return items;
    }

    public async Task<Permission?> GetPermissionByIdAsync(GetPermissionByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<Permission>(
            StoreProcedurePermissionEnum.PermissionGetById.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault();
    }

    public async Task<Permission?> GetPermissionByCodeAsync(GetPermissionByCodeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<Permission>(
            StoreProcedurePermissionEnum.PermissionGetByCode.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault();
    }

    public async Task CreatePermissionAsync(CreatePermissionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("PermissionCreate SP {StoredProcedure}, payload {@Request}", StoreProcedurePermissionEnum.PermissionCreate.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedurePermissionEnum.PermissionCreate.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task UpdatePermissionAsync(UpdatePermissionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("PermissionUpdate SP {StoredProcedure}, payload {@Payload}", StoreProcedurePermissionEnum.PermissionUpdate.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedurePermissionEnum.PermissionUpdate.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task DeletePermissionAsync(DeletePermissionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("PermissionDelete SP {StoredProcedure}, id {PermissionId}", StoreProcedurePermissionEnum.PermissionDelete.ToProcedureString(), request.Id);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedurePermissionEnum.PermissionDelete.ToProcedureString(),
            request,
            cancellationToken);
    }
}

