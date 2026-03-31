using MediatR;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v2.Products.Queries.GetAllProductExportXlsx;

public sealed record ExportProductQuery(ExportProductRequest Request) : IRequest<ExportProductResponse>;

public sealed class ExportProductQueryHandler(
    IProductRepositoryV2 productRepository,
    IProductDocument productDocument,
    ILogger<ExportProductQueryHandler> logger)
    : IRequestHandler<ExportProductQuery, ExportProductResponse>
{
    public async Task<ExportProductResponse> Handle(ExportProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Export product query started.");

        var products = await productRepository.GetAllAsync(
            new GetAllProductRequest { PageNumber = 1, PageSize = 100 },
            cancellationToken);

        var xlsxPath = await productDocument.ExportProductAsync(products, cancellationToken);

        logger.LogInformation("Export product query finished: {FilePath}", xlsxPath);
        return new ExportProductResponse { FilePath = xlsxPath };
    }
}