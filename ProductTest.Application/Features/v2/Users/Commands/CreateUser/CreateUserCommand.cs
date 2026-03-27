using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.CreateUser;

public sealed record CreateUserCommand(CreateUserRequest Request) : IRequest<CreateUserResponse>;

public sealed class CreateUserCommandHandler(IUserRepositoryV2 userRepository, IMapper mapper)
    : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await userRepository.CreateUserAsync(request.Request, cancellationToken);
        return new CreateUserResponse { Success = true };
    }
}

