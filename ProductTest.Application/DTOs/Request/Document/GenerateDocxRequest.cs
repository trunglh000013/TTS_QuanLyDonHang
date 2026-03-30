using System.ComponentModel.DataAnnotations;

namespace ProductTest.Application.DTOs.Request.Document
{
    /// <summary>
    /// Represents a request DTO for generating a DOCX document.
    /// Extend this class with properties as needed for custom document generation.
    /// </summary>
    public sealed record GenerateDocxRequest
    {
        [Required]
        [StringLength(255)]
        public string FilePath { get; init; } = string.Empty;
    }
}