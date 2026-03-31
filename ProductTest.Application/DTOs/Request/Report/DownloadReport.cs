using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Report;

public sealed record DownloadReportRequest
{
    [Required]
    [StringLength(255)]
    public string FileToken { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;
}