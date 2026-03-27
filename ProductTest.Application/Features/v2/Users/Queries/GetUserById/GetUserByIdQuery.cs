using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(GetUserByIdRequest Request) : IRequest<GetUserByIdResponse>;

public sealed class GetUserByIdQueryHandler(IUserRepositoryV2 userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Request, cancellationToken);
        return new GetUserByIdResponse { User = mapper.Map<UserDto>(user) };
    }
}
