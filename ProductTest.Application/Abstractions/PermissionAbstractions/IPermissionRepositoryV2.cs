using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.PermissionAbstractions;

public interface IPermissionRepositoryV2
{
    Task<List<Permission>> GetAllPermissionsAsync(GetAllPermissionRequest request, CancellationToken cancellationToken = default);
    Task<Permission?> GetPermissionByIdAsync(GetPermissionByIdRequest request, CancellationToken cancellationToken = default);
    Task<Permission?> GetPermissionByCodeAsync(GetPermissionByCodeRequest request, CancellationToken cancellationToken = default);
    Task CreatePermissionAsync(CreatePermissionRequest request, CancellationToken cancellationToken = default);
    Task UpdatePermissionAsync(UpdatePermissionRequest request, CancellationToken cancellationToken = default);
    Task DeletePermissionAsync(DeletePermissionRequest request, CancellationToken cancellationToken = default);
}

