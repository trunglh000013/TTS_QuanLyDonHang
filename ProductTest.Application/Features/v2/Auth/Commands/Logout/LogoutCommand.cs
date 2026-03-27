using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.Auth;
using ProductTest.Application.DTOs.Response.Auth;
using ProductTest.Application.DTOs.Request.User;

namespace ProductTest.Application.Features.v2.Auth.Commands.Logout;

public sealed record LogoutCommand(LogoutRequest Request) : IRequest<LogoutResponse>;

public sealed class LogoutCommandHandler(
    IUserRepositoryV2 userRepository,
    IMapper mapper,
    ILogger<LogoutCommandHandler> logger)
    : IRequestHandler<LogoutCommand, LogoutResponse>
{
    public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await userRepository.LogoutAsync(
            mapper.Map<LogoutUserRequest>(request.Request),
            cancellationToken);

        logger.LogInformation("Logout (token revoke) completed. refreshToken={RefreshToken}", request.Request.RefreshToken);

        return new LogoutResponse { Success = true };
    }
}