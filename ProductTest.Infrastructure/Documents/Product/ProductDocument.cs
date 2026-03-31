using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.ProductAbstractions;
using Aspose.Cells;
using ProductTest.Domain.Entities;
using Microsoft.Extensions.Configuration;
using ProductTest.Application.Abstractions.Helpers;

namespace ProductTest.Infrastructure.Documents;

public sealed class ProductDocument(
    ILogger<ProductDocument> logger,
    IConfiguration configuration,
    IDocumentExporter documentExporter) : IProductDocument
{
    public async Task<string> ExportProductAsync(List<Product> data, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(data);

        cancellationToken.ThrowIfCancellationRequested();

        var templatePath = configuration.GetSection("Template:Product:FilePath").Value;
        var exportPath = configuration.GetSection("Export:Product:FilePath").Value;

        return await documentExporter.ExportToXlsxAsync<Product>(
            data,
            Path.Combine(Directory.GetCurrentDirectory(), templatePath),
            Path.Combine(Directory.GetCurrentDirectory(), exportPath),
            ProductExcelMapper.Map,
            cancellationToken
        );
    }
}