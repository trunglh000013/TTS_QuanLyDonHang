using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.UserPermissionAbstractions;
using ProductTest.Application.DTOs.Request.UserPermission;
using ProductTest.Application.DTOs.Response.UserPermission;

namespace ProductTest.Application.Features.v2.UserPermissions.Queries.GetUserPermissionsByPermissionId;

public sealed record GetUserPermissionsByPermissionIdQuery(GetUserPermissionsByPermissionIdRequest Request) : IRequest<GetUserPermissionsByPermissionIdResponse>;

public sealed class GetUserPermissionsByPermissionIdQueryHandler(
    IUserPermissionRepositoryV2 userPermissionRepository,
    IMapper mapper)
    : IRequestHandler<GetUserPermissionsByPermissionIdQuery, GetUserPermissionsByPermissionIdResponse>
{
    public async Task<GetUserPermissionsByPermissionIdResponse> Handle(GetUserPermissionsByPermissionIdQuery request, CancellationToken cancellationToken)
    {
        var items = await userPermissionRepository.GetByPermissionIdAsync(request.Request, cancellationToken);
        return new GetUserPermissionsByPermissionIdResponse { Items = mapper.Map<List<UserPermissionDto>>(items) };
    }
}

