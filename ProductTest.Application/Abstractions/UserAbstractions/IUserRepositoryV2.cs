using ProductTest.Application.DTOs.Request.User;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.UserAbstractions;

public interface IUserRepositoryV2
{
    Task<List<User>> GetAllUsersAsync(GetAllUserRequest request, CancellationToken cancellationToken = default);
    Task<User> GetUserByIdAsync(GetUserByIdRequest request, CancellationToken cancellationToken = default);
    Task<User> GetUserByEmailAsync(GetUserByEmailRequest request, CancellationToken cancellationToken = default);
    Task CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken = default);
    Task GrantRoleAsync(GrantRoleUserRequest request, CancellationToken cancellationToken = default);
    Task RevokeRoleAsync(RevokeRoleUserRequest request, CancellationToken cancellationToken = default);
    Task GrantPermissionAsync(GrantPermissionUserRequest request, CancellationToken cancellationToken = default);
    Task RevokePermissionAsync(RevokePermissionUserRequest request, CancellationToken cancellationToken = default);
    Task LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default);
    Task LogoutAsync(LogoutUserRequest request, CancellationToken cancellationToken = default);
    Task RefreshTokenAsync(RefreshTokenUserRequest request, CancellationToken cancellationToken = default);
    Task RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}

