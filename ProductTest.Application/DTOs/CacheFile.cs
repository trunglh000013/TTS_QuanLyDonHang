namespace ProductTest.Application.DTOs;

public sealed record CachedFile
{
    public byte[] Content { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}