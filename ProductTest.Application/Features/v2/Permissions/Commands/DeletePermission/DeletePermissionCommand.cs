using MediatR;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;

namespace ProductTest.Application.Features.v2.Permissions.Commands.DeletePermission;

public sealed record DeletePermissionCommand(DeletePermissionRequest Request) : IRequest<DeletePermissionResponse>;

public sealed class DeletePermissionCommandHandler(IPermissionRepositoryV2 permissionRepository)
    : IRequestHandler<DeletePermissionCommand, DeletePermissionResponse>
{
    public async Task<DeletePermissionResponse> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await permissionRepository.DeletePermissionAsync(request.Request, cancellationToken);
            return new DeletePermissionResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

