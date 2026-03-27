using MediatR;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;

namespace ProductTest.Application.Features.v2.Permissions.Commands.CreatePermission;

public sealed record CreatePermissionCommand(CreatePermissionRequest Request) : IRequest<CreatePermissionResponse>;

public sealed class CreatePermissionCommandHandler(IPermissionRepositoryV2 permissionRepository)
    : IRequestHandler<CreatePermissionCommand, CreatePermissionResponse>
{
    public async Task<CreatePermissionResponse> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await permissionRepository.CreatePermissionAsync(request.Request, cancellationToken);
            return new CreatePermissionResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}