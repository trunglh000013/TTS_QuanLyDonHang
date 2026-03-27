using MediatR;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(UpdateUserRequest Request) : IRequest<UpdateUserResponse>;

public sealed class UpdateUserCommandHandler(IUserRepositoryV2 userRepository)
    : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.UpdateUserAsync(request.Request, cancellationToken);
            return new UpdateUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

