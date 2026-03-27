using MediatR;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;

namespace ProductTest.Application.Features.v2.Permissions.Commands.UpdatePermission;

public sealed record UpdatePermissionCommand(UpdatePermissionRequest Request) : IRequest<UpdatePermissionResponse>;

public sealed class UpdatePermissionCommandHandler(IPermissionRepositoryV2 permissionRepository)
    : IRequestHandler<UpdatePermissionCommand, UpdatePermissionResponse>
{
    public async Task<UpdatePermissionResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await permissionRepository.UpdatePermissionAsync(request.Request, cancellationToken);
            return new UpdatePermissionResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

