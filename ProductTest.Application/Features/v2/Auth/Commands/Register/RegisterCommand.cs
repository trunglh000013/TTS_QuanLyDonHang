using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.Auth;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.Auth;

namespace ProductTest.Application.Features.v2.Auth.Commands.Register;

public sealed record RegisterCommand(RegisterRequest Request) : IRequest<RegisterResponse>;

public sealed class RegisterCommandHandler(
    IUserRepositoryV2 userRepository,
    IPasswordHasher passwordHasher,
    IMapper mapper,
    ILogger<RegisterCommandHandler> logger)
    : IRequestHandler<RegisterCommand, RegisterResponse>
{
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Register user request received. username={Username}, email={Email}", request.Request.Username, request.Request.Email);

        try
        {
            var hashedPassword = passwordHasher.HashPassword(request.Request.Password);
            request.Request.Password = hashedPassword;

            await userRepository.RegisterAsync(
            mapper.Map<RegisterUserRequest>(request.Request),
            cancellationToken);

            return new RegisterResponse
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}