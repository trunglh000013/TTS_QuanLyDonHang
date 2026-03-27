using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.RolePermissionAbstractions;
using ProductTest.Application.DTOs.Request.RolePermission;
using ProductTest.Application.DTOs.Response.RolePermission;

namespace ProductTest.Application.Features.v2.RolePermissions.Queries.GetRolePermissionsByPermissionId;

public sealed record GetRolePermissionsByPermissionIdQuery(GetRolePermissionsByPermissionIdRequest Request) : IRequest<GetRolePermissionsByPermissionIdResponse>;

public sealed class GetRolePermissionsByPermissionIdQueryHandler(IRolePermissionRepositoryV2 rolePermissionRepository,
    IMapper mapper)
    : IRequestHandler<GetRolePermissionsByPermissionIdQuery, GetRolePermissionsByPermissionIdResponse>
{
    public async Task<GetRolePermissionsByPermissionIdResponse> Handle(GetRolePermissionsByPermissionIdQuery request, CancellationToken cancellationToken)
    {
        var items = await rolePermissionRepository.GetByPermissionIdAsync(request.Request, cancellationToken);
        return new GetRolePermissionsByPermissionIdResponse { Items = mapper.Map<List<RolePermissionDto>>(items) };
    }
}

