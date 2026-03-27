using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Abstractions.UserRoleAbstractions;
using ProductTest.Application.Abstractions.UserTokenAbstractions;
using ProductTest.Application.DTOs.Request.Auth;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Application.DTOs.Response.Auth;

namespace ProductTest.Application.Features.v2.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<RefreshTokenResponse>;

public sealed class RefreshTokenCommandHandler(
    IUserRepositoryV2 userRepository,
    IUserTokenRepositoryV2 userTokenRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRoleRepositoryV2 userRoleRepository,
    IConfiguration configuration,
    ILogger<RefreshTokenCommandHandler> logger)
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await userTokenRepository.GetByRefreshTokenAsync(
            new GetUserTokenByRefreshTokenRequest { RefreshToken = request.Request.RefreshToken },
            cancellationToken);

        if (token is null || !token.IsActive)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        if (token.ExpiresAt.HasValue && token.ExpiresAt.Value < DateTime.UtcNow)
        {
            await userRepository.RefreshTokenAsync(
                new RefreshTokenUserRequest { RefreshToken = request.Request.RefreshToken },
                cancellationToken);

            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        var user = await userRepository.GetUserByIdAsync(
            new GetUserByIdRequest { Id = token.UserId },
            cancellationToken);

        if (user is null || !user.IsActive)
            throw new UnauthorizedAccessException("User is inactive");

        var userRoles = await userRoleRepository.GetByUserIdAsync(
            new GetUserRolesByUserIdRequest { UserId = user.Id },
            cancellationToken);

        var roles = userRoles.Select(r => r.RoleCode).Distinct().ToList();
        if (roles.Count == 0)
            throw new UnauthorizedAccessException("User has no roles");

        var accessToken = jwtTokenGenerator.GenerateAccessToken(user.Id, user.Email, roles);

        logger.LogInformation("Refresh token rotated successfully for userId {UserId}", user.Id);

        return new RefreshTokenResponse
        {
            AccessToken = accessToken
        };
    }
}