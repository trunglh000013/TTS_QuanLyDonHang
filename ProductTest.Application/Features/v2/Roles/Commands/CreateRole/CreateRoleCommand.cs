using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(CreateRoleRequest Request) : IRequest<CreateRoleResponse>;

public sealed class CreateRoleCommandHandler(IRoleRepositoryV2 roleRepository)
    : IRequestHandler<CreateRoleCommand, CreateRoleResponse>
{
    public async Task<CreateRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await roleRepository.CreateRoleAsync(request.Request, cancellationToken);
            return new CreateRoleResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

