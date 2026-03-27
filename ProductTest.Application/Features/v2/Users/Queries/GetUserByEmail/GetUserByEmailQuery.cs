using MediatR;
using AutoMapper;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.User;

namespace ProductTest.Application.Features.v2.Users.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(GetUserByEmailRequest Request) : IRequest<GetUserByEmailResponse>;

public sealed class GetUserByEmailQueryHandler(IUserRepositoryV2 userRepository, IMapper mapper)
    : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>
{
    public async Task<GetUserByEmailResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Request, cancellationToken);
        return new GetUserByEmailResponse { User = mapper.Map<UserDto>(user) };
    }
}

