using AutoMapper;
using MediatR;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;
using ProductTest.Application.Abstractions.ProductAbstractions;

namespace ProductTest.Application.Features.v1.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    UpdateProductRequest Request) : IRequest<UpdateProductResponse>;

public sealed class UpdateProductCommandHandler(
    IProductRepositoryV1 productRepository)
    : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var checkProductName = await productRepository.GetByNameAsync(new GetProductByNameRequest { Name = request.Request.Body?.Name ?? string.Empty }, cancellationToken);
        if (checkProductName is not null && checkProductName.Id != request.Request.Id)
        {
            throw new ConflictException("Product name already exists");
        }

        if (request.Request.Body?.ExpiredDT < DateTime.UtcNow)
        {
            throw new ConflictException("Expired date must be greater than current date");
        }

        var product = await productRepository.GetByIdAsync(new GetProductByIdRequest { Id = request.Request.Id }, cancellationToken);

        if (product is null)
        {
            return new UpdateProductResponse { Success = false };
        }

        await productRepository.UpdateAsync(request.Request, cancellationToken);

        return new UpdateProductResponse { Success = true };
    }
}
