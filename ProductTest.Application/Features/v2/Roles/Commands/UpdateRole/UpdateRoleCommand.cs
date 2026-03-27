using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Commands.UpdateRole;

public sealed record UpdateRoleCommand(UpdateRoleRequest Request) : IRequest<UpdateRoleResponse>;

public sealed class UpdateRoleCommandHandler(IRoleRepositoryV2 roleRepository)
    : IRequestHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await roleRepository.UpdateRoleAsync(request.Request, cancellationToken);
            return new UpdateRoleResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

