namespace ProductTest.Application.DTOs.Response.Report;

public record ReportResponse
{
    public string FileName { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
    public string FileToken { get; set; } = string.Empty;
}