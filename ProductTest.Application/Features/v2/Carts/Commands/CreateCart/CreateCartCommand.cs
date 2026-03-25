using MediatR;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.DTOs.Response.Cart;

namespace ProductTest.Application.Features.v2.Carts.Commands.CreateCart;

public sealed record CreateCartCommand(CreateCartRequest Request) : IRequest<CreateCartResponse>;

public sealed class CreateCartCommandHandler(
    ICartRepositoryV2 cartRepository)
    : IRequestHandler<CreateCartCommand, CreateCartResponse>
{
    public async Task<CreateCartResponse> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await cartRepository.CreateCartAsync(request.Request, cancellationToken);
            return new CreateCartResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}

