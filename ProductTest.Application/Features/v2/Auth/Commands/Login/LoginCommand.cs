using MediatR;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Abstractions.UserRoleAbstractions;
using ProductTest.Application.DTOs.Request.Auth;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Application.DTOs.Response.Auth;

namespace ProductTest.Application.Features.v2.Auth.Commands.Login;

public sealed record LoginCommand(LoginRequest Request) : IRequest<LoginResponse>;

public sealed class LoginCommandHandler(
    IMapper mapper,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepositoryV2 userRepository,
    IUserRoleRepositoryV2 userRoleRepository,
    IConfiguration configuration,
    ILogger<LoginCommandHandler> logger)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(
            mapper.Map<GetUserByEmailRequest>(request.Request),
            cancellationToken);

        if (user is null || !user.IsActive)
            throw new UnauthorizedAccessException("Invalid email or password");

        if (!passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Request.Password))
            throw new UnauthorizedAccessException("Invalid email or password");

        var userRoles = await userRoleRepository.GetByUserIdAsync(
            new GetUserRolesByUserIdRequest { UserId = user.Id },
            cancellationToken);

        if (userRoles.Count == 0)
            throw new UnauthorizedAccessException("User has no roles");

        var roles = userRoles.Select(x => x.RoleCode).Distinct().ToList();

        var accessToken = jwtTokenGenerator.GenerateAccessToken(user.Id, user.Email, roles);
        var refreshToken = jwtTokenGenerator.GenerateRefreshToken();
        var issuedAt = DateTime.UtcNow;
        var refreshTokenExpiry = issuedAt.AddDays(int.Parse(configuration["Jwt:RefreshTokenExpireDays"] ?? "7"));

        logger.LogInformation("Login succeeded for {Email}", user.Email);

        await userRepository.LoginAsync(
            new LoginUserRequest
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                IssuedAt = issuedAt,
                ExpiresAt = refreshTokenExpiry
            },
            cancellationToken);

        var response = new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiry = refreshTokenExpiry,
            UserId = user.Id,
            Email = user.Email,
            Username = user.Username,
            Roles = roles
        };

        return response;
    }
}