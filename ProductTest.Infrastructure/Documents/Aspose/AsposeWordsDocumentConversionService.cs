using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Words;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions.DocumentAbstractions;
using ProductTest.Application.DTOs.Request.Document;

namespace ProductTest.Infrastructure.Documents.AsposeIntegration;

public sealed class AsposeWordsDocumentConversionService(
    ILogger<AsposeWordsDocumentConversionService> logger)
    : IDocumentConversionService
{
    public async Task<string> GenerateDocxAsync(GenerateDocxRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            logger.LogDebug("Generating DOCX document with Aspose.Words.");

            if (File.Exists(request.FilePath))
            {
                File.Delete(request.FilePath);
                logger.LogInformation("Existing file {FilePath} found and deleted.", request.FilePath);
            }

            var document = new Document();
            var builder = new DocumentBuilder(document);
            builder.Writeln("Hello, World!");

            document.Save(request.FilePath, SaveFormat.Docx);

            logger.LogInformation("DOCX document generated successfully and saved to {FilePath}.", request.FilePath);

            return request.FilePath;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to generate DOCX document.");
            throw;
        }
    }
}

