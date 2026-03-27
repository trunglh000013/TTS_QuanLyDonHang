using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Commands.DeleteRole;

public sealed record DeleteRoleCommand(DeleteRoleRequest Request) : IRequest<DeleteRoleResponse>;

public sealed class DeleteRoleCommandHandler(IRoleRepositoryV2 roleRepository)
    : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await roleRepository.DeleteRoleAsync(request.Request, cancellationToken);
            return new DeleteRoleResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

