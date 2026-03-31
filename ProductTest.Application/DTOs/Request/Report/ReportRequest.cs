using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProductTest.Application.DTOs.Request.Report;

public sealed record ReportRequest
{
    [Required]
    public DataSet Data { get; set; } = new DataSet();

    [Required]
    [StringLength(255)]
    public string TemplateFilePath { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;
}