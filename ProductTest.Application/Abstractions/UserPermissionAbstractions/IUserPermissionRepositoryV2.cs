using ProductTest.Application.DTOs.Request.UserPermission;
using ProductTest.Application.DTOs.Response.UserPermission;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.UserPermissionAbstractions;

public interface IUserPermissionRepositoryV2
{
    Task<List<UserPermission>> GetByUserIdAsync(GetUserPermissionsByUserIdRequest request, CancellationToken cancellationToken = default);
    Task<List<UserPermission>> GetByPermissionIdAsync(GetUserPermissionsByPermissionIdRequest request, CancellationToken cancellationToken = default);
}
