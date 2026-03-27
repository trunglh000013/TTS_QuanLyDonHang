using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.RolePermissionAbstractions;
using ProductTest.Application.DTOs.Request.RolePermission;
using ProductTest.Application.DTOs.Response.RolePermission;

namespace ProductTest.Application.Features.v2.RolePermissions.Queries.GetRolePermissionsByRoleId;

public sealed record GetRolePermissionsByRoleIdQuery(GetRolePermissionsByRoleIdRequest Request) : IRequest<GetRolePermissionsByRoleIdResponse>;

public sealed class GetRolePermissionsByRoleIdQueryHandler(IRolePermissionRepositoryV2 rolePermissionRepository,
    IMapper mapper)
    : IRequestHandler<GetRolePermissionsByRoleIdQuery, GetRolePermissionsByRoleIdResponse>
{
    public async Task<GetRolePermissionsByRoleIdResponse> Handle(GetRolePermissionsByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var items = await rolePermissionRepository.GetByRoleIdAsync(request.Request, cancellationToken);
        return new GetRolePermissionsByRoleIdResponse { Items = mapper.Map<List<RolePermissionDto>>(items) };
    }
}

