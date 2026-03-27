using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.UserRoleAbstractions;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.UserRoleRepository;

public sealed class UserRoleRepositoryV2(
    ILogger<UserRoleRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IUserRoleRepositoryV2
{
    public async Task<List<UserRole>> GetByUserIdAsync(GetUserRolesByUserIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<UserRole>(
            StoreProcedureUserRoleEnum.UserRoleGetByUserId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }

    public async Task<List<UserRole>> GetByRoleIdAsync(GetUserRolesByRoleIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<UserRole>(
            StoreProcedureUserRoleEnum.UserRoleGetByRoleId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }
}

