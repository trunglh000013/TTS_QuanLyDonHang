using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.DTOs.Request.Role;
using ProductTest.Application.DTOs.Response.Role;

namespace ProductTest.Application.Features.v2.Roles.Queries.GetRoleByCode;

public sealed record GetRoleByCodeQuery(GetRoleByCodeRequest Request) : IRequest<GetRoleByCodeResponse>;

public sealed class GetRoleByCodeQueryHandler(IRoleRepositoryV2 roleRepository, IMapper mapper)
    : IRequestHandler<GetRoleByCodeQuery, GetRoleByCodeResponse>
{
    public async Task<GetRoleByCodeResponse> Handle(GetRoleByCodeQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetRoleByCodeAsync(request.Request, cancellationToken);
        return new GetRoleByCodeResponse { Role = mapper.Map<RoleDto>(role) };
    }
}

