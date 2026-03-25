using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.DTOs.Response.Cart;

namespace ProductTest.Application.Features.v2.Carts.Queries.GetAllItemByCustomerIdCart;

public sealed record GetAllItemByCustomerIdCartQuery(GetAllItemCartByCustomerIdRequest Request) : IRequest<GetAllItemByCustomerIdCartResponse>;

public sealed class GetAllItemByCustomerIdCartQueryHandler(
    ICartRepositoryV2 cartRepository,
    IMapper mapper)
    : IRequestHandler<GetAllItemByCustomerIdCartQuery, GetAllItemByCustomerIdCartResponse>
{
    public async Task<GetAllItemByCustomerIdCartResponse> Handle(GetAllItemByCustomerIdCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAllIteamCartByCustomerIdAsync(request.Request, cancellationToken);
        var cartItems = mapper.Map<List<CartItemDto>>(cart);
        return new GetAllItemByCustomerIdCartResponse { Items = cartItems, PageNumber = request.Request.PageNumber, PageSize = request.Request.PageSize, TotalCount = cart.Count };
    }
}
