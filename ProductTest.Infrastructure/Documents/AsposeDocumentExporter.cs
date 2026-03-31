using Aspose.Cells;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Infrastructure.Documents;

public class AsposeDocumentExporter : IDocumentExporter
{
    public async Task<string> ExportToXlsxAsync<T>(
        IEnumerable<T> data,
        string templatePath,
        string outputDirectory,
        Action<IExcelRowWriter, T, int> mapRow,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);

        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Template file not found at {templatePath}");

        if (!Directory.Exists(outputDirectory))
            Directory.CreateDirectory(outputDirectory);

        var fileName = $"export-{typeof(T).Name}-{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.xlsx";
        var filePath = Path.Combine(outputDirectory, fileName);

        var workbook = new Workbook(templatePath);
        var worksheet = workbook.Worksheets[0];

        int row = 1;

        var writer = new AsposeExcelRowWriter(worksheet);

        foreach (var item in data)
        {
            mapRow(writer, item, row);
            row++;
        }

        worksheet.AutoFitColumns();
        workbook.Save(filePath, SaveFormat.Xlsx);

        return filePath;
    }
}