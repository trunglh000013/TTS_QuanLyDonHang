using MediatR;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Commands.GrantRole;

public sealed record GrantRoleCommand(GrantRoleUserRequest Request) : IRequest<GrantRoleUserResponse>;

public sealed class GrantRoleCommandHandler(IUserRepositoryV2 userRepository)
    : IRequestHandler<GrantRoleCommand, GrantRoleUserResponse>
{
    public async Task<GrantRoleUserResponse> Handle(GrantRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.GrantRoleAsync(
                request.Request,
                cancellationToken);
            return new GrantRoleUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
