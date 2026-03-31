using System.Threading;
using System.Threading.Tasks;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Abstractions.ProductAbstractions;

public interface IProductDocument
{
    /// <summary>
    /// Gets all product data and exports them to an XLSX document.
    /// </summary>
    /// <param name="data">The product data to export.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The file path of the generated XLSX document.</returns>
    Task<string> ExportProductAsync(List<Product> data, CancellationToken cancellationToken);
}