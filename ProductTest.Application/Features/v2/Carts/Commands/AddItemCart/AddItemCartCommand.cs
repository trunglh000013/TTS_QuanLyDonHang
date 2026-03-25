using MediatR;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.DTOs.Response.Cart;

namespace ProductTest.Application.Features.v2.Carts.Commands.AddItemCart;

public sealed record AddItemCartCommand(AddItemCartRequest Request) : IRequest<AddItemCartResponse>;

public sealed class AddItemCartCommandHandler(
    ICartRepositoryV2 cartRepository)
    : IRequestHandler<AddItemCartCommand, AddItemCartResponse>
{
    public async Task<AddItemCartResponse> Handle(AddItemCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await cartRepository.AddItemCartAsync(request.Request, cancellationToken);
            return new AddItemCartResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}