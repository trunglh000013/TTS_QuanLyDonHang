using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;

namespace ProductTest.Application.Features.v2.Permissions.Queries.GetPermissionByCode;

public sealed record GetPermissionByCodeQuery(GetPermissionByCodeRequest Request) : IRequest<GetPermissionByCodeResponse>;

public sealed class GetPermissionByCodeQueryHandler(IPermissionRepositoryV2 permissionRepository, IMapper mapper)
    : IRequestHandler<GetPermissionByCodeQuery, GetPermissionByCodeResponse>
{
    public async Task<GetPermissionByCodeResponse> Handle(GetPermissionByCodeQuery request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetPermissionByCodeAsync(request.Request, cancellationToken);
        return new GetPermissionByCodeResponse { Permission = mapper.Map<PermissionDto>(permission) };
    }
}

