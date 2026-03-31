using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.RolePermissionAbstractions;
using ProductTest.Application.DTOs.Request.RolePermission;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.RolePermissionRepository;

public sealed class RolePermissionRepositoryV2(
    ILogger<RolePermissionRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IRolePermissionRepositoryV2
{
    public async Task<List<RolePermission>> GetByPermissionIdAsync(GetRolePermissionsByPermissionIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<RolePermission>(
            StoreProcedureRolePermissionEnum.RolePermissionGetByPermissionId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }

    public async Task<List<RolePermission>> GetByRoleIdAsync(GetRolePermissionsByRoleIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<RolePermission>(
            StoreProcedureRolePermissionEnum.RolePermissionGetByRoleId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }
}

