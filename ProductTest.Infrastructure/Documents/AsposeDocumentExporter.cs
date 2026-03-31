using System.Data;
using Aspose.Cells;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.DTOs.Request.Report;
using ProductTest.Infrastructure.Documents;
using ProductTest.Application.DTOs.Response.Report;
using Microsoft.Extensions.Caching.Memory;
using ProductTest.Application.DTOs;
using Microsoft.Extensions.Logging;

public class AsposeDocumentExporter(ILogger<AsposeDocumentExporter> logger, IMemoryCache memoryCache) : IDocumentExporter
{
    public async Task<ReportResponse> ExportToXlsxAsync(
        ReportRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(request.TemplateFilePath))
            throw new FileNotFoundException($"Template file not found at {request.TemplateFilePath}");

        Workbook workbook = new Workbook(request.TemplateFilePath);
        WorkbookDesigner designWord = new WorkbookDesigner(workbook);
        foreach (DataTable table in request.Data.Tables)
        {
            logger.LogInformation("Exporting table: {TableName} with {RowCount} rows", table.TableName, table.Rows.Count);
            designWord.SetDataSource(table.TableName, table);
        }
        designWord.SetDataSource(request.Data);
        designWord.Process(false);
        designWord.Workbook.FileName = request.FileName;
        designWord.Workbook.FileFormat = FileFormatType.Xlsx;
        designWord.Workbook.Settings.FormulaSettings.CalculationMode = CalcModeType.Automatic;
        designWord.Workbook.Settings.FormulaSettings.CalculateOnSave = true;
        designWord.Workbook.Settings.FormulaSettings.CalculateOnOpen = true;
        designWord.Workbook.Settings.CheckCustomNumberFormat = true;
        designWord.Workbook.Worksheets[0].AutoFitRows();

        using var memoryStream = new MemoryStream();
        designWord.Workbook.Save(memoryStream, SaveFormat.Xlsx);
        var bytes = memoryStream.ToArray();

        var fileToken = Guid.NewGuid().ToString();
        memoryCache.Set(
            fileToken,
            new CachedFile
            {
                Content = bytes,
                FileName = request.FileName,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            },
            TimeSpan.FromMinutes(10)
        );

        logger.LogInformation("File exported successfully and cached with key: {FileToken}, file name: {FileName}, file size: {FileSize}", fileToken, request.FileName, bytes.Length);

        return new ReportResponse
        {
            FileName = request.FileName,
            FileToken = fileToken
        };
    }
}