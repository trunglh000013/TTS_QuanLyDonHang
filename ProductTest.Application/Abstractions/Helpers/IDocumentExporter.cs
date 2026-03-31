using ProductTest.Application.DTOs.Request.Report;
using ProductTest.Application.DTOs.Response.Report;

namespace ProductTest.Application.Abstractions.Helpers;

public interface IDocumentExporter
{
    Task<ReportResponse> ExportToXlsxAsync(
        ReportRequest request,
        CancellationToken cancellationToken = default
    );
}