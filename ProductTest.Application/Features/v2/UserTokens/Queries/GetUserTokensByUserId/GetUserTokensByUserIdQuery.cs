using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.UserTokenAbstractions;
using ProductTest.Application.DTOs.Request.UserToken;
using ProductTest.Application.DTOs.Response.UserToken;

namespace ProductTest.Application.Features.v2.UserTokens.Queries.GetUserTokensByUserId;

public sealed record GetUserTokensByUserIdQuery(GetUserTokensByUserIdRequest Request) : IRequest<GetUserTokensByUserIdResponse>;

public sealed class GetUserTokensByUserIdQueryHandler(IUserTokenRepositoryV2 userTokenRepository,
    IMapper mapper)
    : IRequestHandler<GetUserTokensByUserIdQuery, GetUserTokensByUserIdResponse>
{
    public async Task<GetUserTokensByUserIdResponse> Handle(GetUserTokensByUserIdQuery request, CancellationToken cancellationToken)
    {
        var items = await userTokenRepository.GetByUserIdAsync(request.Request, cancellationToken);
        return new GetUserTokensByUserIdResponse { Items = mapper.Map<List<UserTokenDto>>(items) };
    }
}

