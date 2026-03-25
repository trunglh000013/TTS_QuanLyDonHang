using AutoMapper;
using MediatR;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v2.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    CreateProductRequest Request) : IRequest<CreateProductResponse>;

public sealed class CreateProductCommandHandler(
    IProductRepositoryV2 productRepository)
    : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var checkProductName = await productRepository.GetByNameAsync(new GetProductByNameRequest { Name = request.Request.Name }, cancellationToken);
            if (checkProductName is not null)
            {
                throw new ConflictException("Product already exists");
            }

            if (request.Request.ExpiredDT < DateTime.UtcNow)
            {
                throw new ConflictException("Expired date must be greater than current date");
            }

            await productRepository.AddAsync(request.Request, cancellationToken);
            return new CreateProductResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
