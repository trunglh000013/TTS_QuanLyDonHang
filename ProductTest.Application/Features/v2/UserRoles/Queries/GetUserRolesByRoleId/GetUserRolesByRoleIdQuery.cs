using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.UserRoleAbstractions;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Application.DTOs.Response.UserRole;

namespace ProductTest.Application.Features.v2.UserRoles.Queries.GetUserRolesByRoleId;

public sealed record GetUserRolesByRoleIdQuery(GetUserRolesByRoleIdRequest Request) : IRequest<GetUserRolesByRoleIdResponse>;

public sealed class GetUserRolesByRoleIdQueryHandler(
    IUserRoleRepositoryV2 userRoleRepository,
    IMapper mapper)
    : IRequestHandler<GetUserRolesByRoleIdQuery, GetUserRolesByRoleIdResponse>
{
    public async Task<GetUserRolesByRoleIdResponse> Handle(GetUserRolesByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var items = await userRoleRepository.GetByRoleIdAsync(request.Request, cancellationToken);
        return new GetUserRolesByRoleIdResponse { Items = mapper.Map<List<UserRoleDto>>(items) };
    }
}

