using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;

namespace ProductTest.Application.Features.v2.Permissions.Queries.GetPermissionById;

public sealed record GetPermissionByIdQuery(GetPermissionByIdRequest Request) : IRequest<GetPermissionByIdResponse>;

public sealed class GetPermissionByIdQueryHandler(IPermissionRepositoryV2 permissionRepository, IMapper mapper)
    : IRequestHandler<GetPermissionByIdQuery, GetPermissionByIdResponse>
{
    public async Task<GetPermissionByIdResponse> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetPermissionByIdAsync(request.Request, cancellationToken);
        return new GetPermissionByIdResponse { Permission = mapper.Map<PermissionDto>(permission) };
    }
}

