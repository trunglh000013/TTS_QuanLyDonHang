using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Queries.GetAllRoles;

public sealed record GetAllRolesQuery(GetAllRoleRequest Request) : IRequest<GetAllRolesResponse>;

public sealed class GetAllRolesQueryHandler(IRoleRepositoryV2 roleRepository,
    IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, GetAllRolesResponse>
{
    public async Task<GetAllRolesResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var items = await roleRepository.GetAllRolesAsync(request.Request, cancellationToken);

        return new GetAllRolesResponse
        {
            Items = mapper.Map<List<RoleDto>>(items),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = items.Count
        };
    }
}

