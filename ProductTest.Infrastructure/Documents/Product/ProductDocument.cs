using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.DTOs.Response.Report;
using ProductTest.Application.DTOs.Request.Report;

namespace ProductTest.Infrastructure.Documents;

public sealed class ProductDocument(
    ILogger<ProductDocument> logger,
    IDocumentExporter documentExporter) : IProductDocument
{
    public async Task<ReportResponse> ExportProductAsync(ReportRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        cancellationToken.ThrowIfCancellationRequested();

        return await documentExporter.ExportToXlsxAsync(
            request,
            cancellationToken
        );
    }
}