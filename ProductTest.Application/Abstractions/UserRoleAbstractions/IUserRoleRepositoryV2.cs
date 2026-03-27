using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.UserRoleAbstractions;

public interface IUserRoleRepositoryV2
{
    Task<List<UserRole>> GetByUserIdAsync(GetUserRolesByUserIdRequest request, CancellationToken cancellationToken = default);
    Task<List<UserRole>> GetByRoleIdAsync(GetUserRolesByRoleIdRequest request, CancellationToken cancellationToken = default);
}
