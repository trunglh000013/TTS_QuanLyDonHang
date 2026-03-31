namespace ProductTest.Application.Abstractions.Helpers;

public interface IDocumentExporter
{
    Task<string> ExportToXlsxAsync<T>(
        IEnumerable<T> data,
        string templatePath,
        string outputPath,
        Action<IExcelRowWriter, T, int> mapRow,
        CancellationToken cancellationToken = default
    );
}