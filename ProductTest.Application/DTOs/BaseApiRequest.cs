namespace ProductTest.Application.DTOs
{
    public sealed record BaseApiRequest
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}