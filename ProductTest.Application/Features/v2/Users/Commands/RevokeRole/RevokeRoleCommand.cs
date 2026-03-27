using MediatR;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.RevokeRole;

public sealed record RevokeRoleCommand(RevokeRoleUserRequest Request) : IRequest<RevokeRoleUserResponse>;

public sealed class RevokeRoleCommandHandler(IUserRepositoryV2 userRepository)
    : IRequestHandler<RevokeRoleCommand, RevokeRoleUserResponse>
{
    public async Task<RevokeRoleUserResponse> Handle(RevokeRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.RevokeRoleAsync(
            request.Request,
            cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }

        return new RevokeRoleUserResponse { Success = true };
    }
}
