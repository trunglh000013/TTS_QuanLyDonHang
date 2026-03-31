using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.UserTokenAbstractions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Request.UserToken;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.UserTokenRepository;

public sealed class UserTokenRepositoryV2(
    ILogger<UserTokenRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IUserTokenRepositoryV2
{
    public async Task<UserToken?> GetByRefreshTokenAsync(GetUserTokenByRefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserTokenGetByRefreshToken SP {StoredProcedure}, payload {@Request}", StoreProcedureUserTokenEnum.UserTokenGetByRefreshToken.ToProcedureString(), request);

        var rows = await spRunner.ExecuteProcedureAsync<UserToken>(
            StoreProcedureUserTokenEnum.UserTokenGetByRefreshToken.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault();
    }

    public async Task<List<UserToken>> GetByUserIdAsync(GetUserTokensByUserIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserTokenGetByUserId SP {StoredProcedure}, payload {@Request}", StoreProcedureUserTokenEnum.UserTokenGetByUserId.ToProcedureString(), request);

        var rows = await spRunner.ExecuteProcedureAsync<UserToken>(
            StoreProcedureUserTokenEnum.UserTokenGetByUserId.ToProcedureString(),
            request,
            cancellationToken);

        return rows;
    }
}
