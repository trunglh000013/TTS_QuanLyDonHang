using MediatR;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v1.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
    DeleteProductRequest Request
) : IRequest<DeleteProductResponse>;

public sealed class DeleteProductCommandHandler(IProductRepositoryV1 productRepository)
    : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(new GetProductByIdRequest { Id = request.Request.Id }, cancellationToken);

        if (product is null)
        {
            return new DeleteProductResponse { Success = false };
        }

        await productRepository.DeleteAsync(request.Request, cancellationToken);
        return new DeleteProductResponse { Success = true };
    }
}
