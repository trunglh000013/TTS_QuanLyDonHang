using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Request.UserToken;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.UserTokenAbstractions;

public interface IUserTokenRepositoryV2
{
    Task<UserToken?> GetByRefreshTokenAsync(GetUserTokenByRefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<List<UserToken>> GetByUserIdAsync(GetUserTokensByUserIdRequest request, CancellationToken cancellationToken = default);
}