using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

namespace ProductTest.Infrastructure.Repositories.UserRepository;

public sealed class UserRepositoryV2(
    ILogger<UserRepositoryV2> logger,
    IStoreProcedureRunner spRunner) : IUserRepositoryV2
{
    public async Task<List<User>> GetAllUsersAsync(GetAllUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var items = await spRunner.ExecuteProcedureAsync<User>(
            StoreProcedureUserEnum.UserGetAll.ToProcedureString(),
            request,
            cancellationToken);

        return items;
    }

    public async Task<User> GetUserByIdAsync(GetUserByIdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<User>(
            StoreProcedureUserEnum.UserGetById.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault() ?? new User();
    }

    public async Task<User> GetUserByEmailAsync(GetUserByEmailRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rows = await spRunner.ExecuteProcedureAsync<User>(
            StoreProcedureUserEnum.UserGetByEmail.ToProcedureString(),
            request,
            cancellationToken);

        return rows.FirstOrDefault() ?? new User();
    }

    public async Task CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserCreate SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserCreate.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserCreate.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserUpdate SP {StoredProcedure}, payload {@Payload}", StoreProcedureUserEnum.UserUpdate.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserUpdate.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserDelete SP {StoredProcedure}, id {UserId}", StoreProcedureUserEnum.UserDelete.ToProcedureString(), request.Id);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserDelete.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task GrantRoleAsync(GrantRoleUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserGrantRole SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserGrantRole.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserGrantRole.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task RevokeRoleAsync(RevokeRoleUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserRevokeRole SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserRevokeRole.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserRevokeRole.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task GrantPermissionAsync(GrantPermissionUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserGrantPermission SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserGrantPermission.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserGrantPermission.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task RevokePermissionAsync(RevokePermissionUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserRevokePermission SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserRevokePermission.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserRevokePermission.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserLogin SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserLogin.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserLogin.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task LogoutAsync(LogoutUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserLogout SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserLogout.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserLogout.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task RefreshTokenAsync(RefreshTokenUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserRefreshToken SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserRefreshToken.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserRefreshToken.ToProcedureString(),
            request,
            cancellationToken);
    }

    public async Task RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("UserRegister SP {StoredProcedure}, payload {@Request}", StoreProcedureUserEnum.UserRegister.ToProcedureString(), request);

        await spRunner.ExecuteNonQueryAsync(
            StoreProcedureUserEnum.UserRegister.ToProcedureString(),
            request,
            cancellationToken);
    }
}

