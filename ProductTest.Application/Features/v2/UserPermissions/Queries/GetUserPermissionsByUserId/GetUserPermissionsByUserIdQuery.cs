using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.UserPermissionAbstractions;
using ProductTest.Application.DTOs.Request.UserPermission;
using ProductTest.Application.DTOs.Response.UserPermission;

namespace ProductTest.Application.Features.v2.UserPermissions.Queries.GetUserPermissionsByUserId;

public sealed record GetUserPermissionsByUserIdQuery(GetUserPermissionsByUserIdRequest Request) : IRequest<GetUserPermissionsByUserIdResponse>;

public sealed class GetUserPermissionsByUserIdQueryHandler(
    IUserPermissionRepositoryV2 userPermissionRepository,
    IMapper mapper)
    : IRequestHandler<GetUserPermissionsByUserIdQuery, GetUserPermissionsByUserIdResponse>
{
    public async Task<GetUserPermissionsByUserIdResponse> Handle(GetUserPermissionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var items = await userPermissionRepository.GetByUserIdAsync(request.Request, cancellationToken);
        return new GetUserPermissionsByUserIdResponse { Items = mapper.Map<List<UserPermissionDto>>(items) };
    }
}

