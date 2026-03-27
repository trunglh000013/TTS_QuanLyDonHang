using MediatR;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.GrantPermission;

public sealed record GrantPermissionCommand(GrantPermissionUserRequest Request) : IRequest<GrantPermissionUserResponse>;

public sealed class GrantPermissionCommandHandler(IUserRepositoryV2 userRepository)
    : IRequestHandler<GrantPermissionCommand, GrantPermissionUserResponse>
{
    public async Task<GrantPermissionUserResponse> Handle(GrantPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.GrantPermissionAsync(
                request.Request,
                cancellationToken);
            return new GrantPermissionUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
