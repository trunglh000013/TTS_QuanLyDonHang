using MediatR;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.DTOs.Response.Cart;

namespace ProductTest.Application.Features.v2.Carts.Commands.DeleteItemCart;

public sealed record DeleteItemCartCommand(DeleteItemCartRequest Request) : IRequest<DeleteItemCartResponse>;

public sealed class DeleteItemCartCommandHandler(
    ICartRepositoryV2 cartRepository)
    : IRequestHandler<DeleteItemCartCommand, DeleteItemCartResponse>
{
    public async Task<DeleteItemCartResponse> Handle(DeleteItemCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await cartRepository.DeleteItemCartAsync(request.Request, cancellationToken);
            return new DeleteItemCartResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}