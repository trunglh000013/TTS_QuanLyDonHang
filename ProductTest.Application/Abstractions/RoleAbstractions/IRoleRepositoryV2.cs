using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.RoleAbstractions;

public interface IRoleRepositoryV2
{
    Task<List<Role>> GetAllRolesAsync(GetAllRoleRequest request, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleByIdAsync(GetRoleByIdRequest request, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleByCodeAsync(GetRoleByCodeRequest request, CancellationToken cancellationToken = default);
    Task CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
    Task UpdateRoleAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default);
    Task DeleteRoleAsync(DeleteRoleRequest request, CancellationToken cancellationToken = default);
    Task GrantPermissionAsync(GrantPermissionRoleRequest request, CancellationToken cancellationToken = default);
    Task RevokePermissionAsync(RevokePermissionRoleRequest request, CancellationToken cancellationToken = default);
}