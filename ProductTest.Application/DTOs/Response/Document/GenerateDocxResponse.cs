namespace ProductTest.Application.DTOs.Response.Document
{
    /// <summary>
    /// Represents a response DTO for DOCX document generation.
    /// Contains the generated DOCX file as a path.
    /// </summary>
    public sealed record GenerateDocxResponse
    {
        public string DocxPath { get; init; } = string.Empty;
    }
}