using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.UserRoleAbstractions;
using ProductTest.Application.DTOs.Request.UserRole;
using ProductTest.Application.DTOs.Response.UserRole;

namespace ProductTest.Application.Features.v2.UserRoles.Queries.GetUserRolesByUserId;

public sealed record GetUserRolesByUserIdQuery(GetUserRolesByUserIdRequest Request) : IRequest<GetUserRolesByUserIdResponse>;

public sealed class GetUserRolesByUserIdQueryHandler(
    IUserRoleRepositoryV2 userRoleRepository,
    IMapper mapper)
    : IRequestHandler<GetUserRolesByUserIdQuery, GetUserRolesByUserIdResponse>
{
    public async Task<GetUserRolesByUserIdResponse> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var items = await userRoleRepository.GetByUserIdAsync(request.Request, cancellationToken);
        return new GetUserRolesByUserIdResponse { Items = mapper.Map<List<UserRoleDto>>(items) };
    }
}