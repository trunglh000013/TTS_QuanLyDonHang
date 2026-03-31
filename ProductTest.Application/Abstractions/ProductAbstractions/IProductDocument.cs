using ProductTest.Application.DTOs.Response.Report;
using ProductTest.Application.DTOs.Request.Report;

namespace ProductTest.Application.Abstractions.ProductAbstractions;

public interface IProductDocument
{
    /// <summary>
    /// Gets all product data and exports them to an XLSX document.
    /// </summary>
    /// <param name="request">The request to export the product.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the file name, content, and token.</returns>
    Task<ReportResponse> ExportProductAsync(ReportRequest request, CancellationToken cancellationToken);
}