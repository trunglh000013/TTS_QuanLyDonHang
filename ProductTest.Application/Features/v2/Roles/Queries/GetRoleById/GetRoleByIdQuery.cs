using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Queries.GetRoleById;

public sealed record GetRoleByIdQuery(GetRoleByIdRequest Request) : IRequest<GetRoleByIdResponse>;

public sealed class GetRoleByIdQueryHandler(IRoleRepositoryV2 roleRepository, IMapper mapper)
    : IRequestHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    public async Task<GetRoleByIdResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetRoleByIdAsync(request.Request, cancellationToken);
        return new GetRoleByIdResponse { Role = mapper.Map<RoleDto>(role) };
    }
}

