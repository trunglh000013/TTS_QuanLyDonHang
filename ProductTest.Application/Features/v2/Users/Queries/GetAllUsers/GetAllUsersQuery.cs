using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery(GetAllUserRequest Request) : IRequest<GetAllUsersResponse>;

public sealed class GetAllUsersQueryHandler(IUserRepositoryV2 userRepository, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var items = await userRepository.GetAllUsersAsync(request.Request, cancellationToken);

        return new GetAllUsersResponse
        {
            Items = mapper.Map<List<UserDto>>(items),
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalCount = items.Count
        };
    }
}

