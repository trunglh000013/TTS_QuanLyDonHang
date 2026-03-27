using MediatR;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.RevokePermission;

public sealed record RevokePermissionCommand(RevokePermissionUserRequest Request) : IRequest<RevokePermissionUserResponse>;

public sealed class RevokePermissionCommandHandler(IUserRepositoryV2 userRepository)
    : IRequestHandler<RevokePermissionCommand, RevokePermissionUserResponse>
{
    public async Task<RevokePermissionUserResponse> Handle(RevokePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.RevokePermissionAsync(
                request.Request,
                cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }

        return new RevokePermissionUserResponse { Success = true };
    }
}