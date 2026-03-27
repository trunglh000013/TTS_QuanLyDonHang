using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Commands.RevokePermission;

public sealed record RevokePermissionCommand(RevokePermissionRoleRequest Request) : IRequest<RevokePermissionRoleResponse>;

public sealed class RevokePermissionCommandHandler(IRoleRepositoryV2 roleRepository)
    : IRequestHandler<RevokePermissionCommand, RevokePermissionRoleResponse>
{
    public async Task<RevokePermissionRoleResponse> Handle(RevokePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await roleRepository.RevokePermissionAsync(
            request.Request,
            cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }

        return new RevokePermissionRoleResponse { Success = true };
    }
}
