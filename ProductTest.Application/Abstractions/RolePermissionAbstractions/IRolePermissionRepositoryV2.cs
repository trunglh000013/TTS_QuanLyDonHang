using ProductTest.Application.DTOs.Request.RolePermission;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.RolePermissionAbstractions;

public interface IRolePermissionRepositoryV2
{
    Task<List<RolePermission>> GetByRoleIdAsync(GetRolePermissionsByRoleIdRequest request, CancellationToken cancellationToken = default);
    Task<List<RolePermission>> GetByPermissionIdAsync(GetRolePermissionsByPermissionIdRequest request, CancellationToken cancellationToken = default);
}
