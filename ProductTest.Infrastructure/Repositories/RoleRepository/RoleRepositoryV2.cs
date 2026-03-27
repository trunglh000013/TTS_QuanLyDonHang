using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.RoleRepository;

public sealed class RoleRepositoryV2(
    ILogger<RoleRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IRoleRepositoryV2
{
    public async Task<List<Role>> GetAllRolesAsync(GetAllRoleRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var items = await spRunner.ExecuteProcedureAsync<Role>(
            StoreProcedureRoleEnum.RoleGetAll.ToProcedureString(),
            request,
            cancellationToken);

        return items;
    }

    public async Task<Role?> GetRoleByIdAsync(GetRoleByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<Role>(
            StoreProcedureRoleEnum.RoleGetById.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault();
    }

    public async Task<Role?> GetRoleByCodeAsync(GetRoleByCodeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<Role>(
            StoreProcedureRoleEnum.RoleGetByCode.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault();
    }

    public async Task CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("RoleCreate SP {StoredProcedure}, payload {@Request}", StoreProcedureRoleEnum.RoleCreate.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureRoleEnum.RoleCreate.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task UpdateRoleAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("RoleUpdate SP {StoredProcedure}, payload {@Payload}", StoreProcedureRoleEnum.RoleUpdate.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureRoleEnum.RoleUpdate.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task DeleteRoleAsync(DeleteRoleRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("RoleDelete SP {StoredProcedure}, id {RoleId}", StoreProcedureRoleEnum.RoleDelete.ToProcedureString(), request.Id);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureRoleEnum.RoleDelete.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task GrantPermissionAsync(GrantPermissionRoleRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("RoleGrantPermission SP {StoredProcedure}, payload {@Request}", StoreProcedureRoleEnum.RoleGrantPermission.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureRoleEnum.RoleGrantPermission.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task RevokePermissionAsync(RevokePermissionRoleRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("RoleRevokePermission SP {StoredProcedure}, payload {@Request}", StoreProcedureRoleEnum.RoleRevokePermission.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureRoleEnum.RoleRevokePermission.ToProcedureString(),
            request,
            cancellationToken);
    }
}

