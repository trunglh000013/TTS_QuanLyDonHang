using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Commands.GrantPermission;

public sealed record GrantPermissionCommand(GrantPermissionRoleRequest Request) : IRequest<GrantPermissionRoleResponse>;

public sealed class GrantPermissionCommandHandler(IRoleRepositoryV2 roleRepository)
    : IRequestHandler<GrantPermissionCommand, GrantPermissionRoleResponse>
{
    public async Task<GrantPermissionRoleResponse> Handle(GrantPermissionCommand request, CancellationToken cancellationToken)
    {

        try
        {
            await roleRepository.GrantPermissionAsync(
                request.Request,
                cancellationToken);

            return new GrantPermissionRoleResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
