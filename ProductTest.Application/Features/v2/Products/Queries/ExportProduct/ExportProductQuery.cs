using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Request.Report;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Features.v2.Products.Queries.GetAllProductExportXlsx;

public sealed record ExportProductQuery(ExportProductRequest Request) : IRequest<ExportProductResponse>;

public sealed class ExportProductQueryHandler(
    IProductRepositoryV2 productRepository,
    IProductDocument productDocument,
    ILogger<ExportProductQueryHandler> logger,
    IConfiguration configuration)
    : IRequestHandler<ExportProductQuery, ExportProductResponse>
{
    public async Task<ExportProductResponse> Handle(ExportProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Export product query started.");

        var products = await productRepository.GetAllToDataSetAsync(
            new GetAllProductRequest { PageNumber = 1, PageSize = 100 },
            cancellationToken);

        var reportRequest = new ReportRequest
        {
            Data = products,
            TemplateFilePath = configuration["Template:Product:FilePath"],
            FileName = "Export-Product-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"
        };
        var reportResponse = await productDocument.ExportProductAsync(reportRequest, cancellationToken);

        logger.LogInformation("Export product query finished: {FilePath}", reportResponse.FileName);
        return new ExportProductResponse { FileName = reportResponse.FileName, FileToken = reportResponse.FileToken };
    }
}