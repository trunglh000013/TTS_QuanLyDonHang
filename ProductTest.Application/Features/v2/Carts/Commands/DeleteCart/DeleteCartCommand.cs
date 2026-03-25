using MediatR;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.DTOs.Response.Cart;

namespace ProductTest.Application.Features.v2.Carts.Commands.DeleteCart
{
    public sealed record DeleteCartCommand(DeleteCartRequest Request) : IRequest<DeleteCartResponse>;

    public sealed class DeleteCartCommandHandler(
        ICartRepositoryV2 cartRepository)
        : IRequestHandler<DeleteCartCommand, DeleteCartResponse>
    {
        public async Task<DeleteCartResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await cartRepository.DeleteCartAsync(request.Request, cancellationToken);
                return new DeleteCartResponse { Success = true };
            }
            catch (Exception ex)
            {
                throw new ConflictException(ex.Message);
            }
        }
    }
}
