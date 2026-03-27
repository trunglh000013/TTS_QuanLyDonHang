using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.DTOs.Request.Permission;
using ProductTest.Application.DTOs.Response.Permission;

namespace ProductTest.Application.Features.v2.Permissions.Queries.GetAllPermissions;

public sealed record GetAllPermissionsQuery(GetAllPermissionRequest Request) : IRequest<GetAllPermissionsResponse>;

public sealed class GetAllPermissionsQueryHandler(IPermissionRepositoryV2 permissionRepository, IMapper mapper)
    : IRequestHandler<GetAllPermissionsQuery, GetAllPermissionsResponse>
{
    public async Task<GetAllPermissionsResponse> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var items = await permissionRepository.GetAllPermissionsAsync(request.Request, cancellationToken);

        return new GetAllPermissionsResponse
        {
            Items = mapper.Map<List<PermissionDto>>(items),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = items.Count
        };
    }
}

