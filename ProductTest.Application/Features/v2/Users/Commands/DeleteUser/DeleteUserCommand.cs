using MediatR;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(DeleteUserRequest Request) : IRequest<DeleteUserResponse>;

public sealed class DeleteUserCommandHandler(IUserRepositoryV2 userRepository)
    : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.DeleteUserAsync(request.Request, cancellationToken);
            return new DeleteUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

