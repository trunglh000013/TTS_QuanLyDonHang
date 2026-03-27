using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.UserPermissionAbstractions;
using ProductTest.Application.DTOs.Request.UserPermission;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.UserPermissionRepository;

public sealed class UserPermissionRepositoryV2(
    ILogger<UserPermissionRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IUserPermissionRepositoryV2
{
    public async Task<List<UserPermission>> GetByUserIdAsync(GetUserPermissionsByUserIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<UserPermission>(
            StoreProcedureUserPermissionEnum.UserPermissionGetByUserId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }

    public async Task<List<UserPermission>> GetByPermissionIdAsync(GetUserPermissionsByPermissionIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<UserPermission>(
            StoreProcedureUserPermissionEnum.UserPermissionGetByPermissionId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }
}

