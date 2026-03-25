namespace ProductTest.Application.DTOs
{
    public sealed record BaseApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string Error { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;

        // Additional properties for middleware compatibility
        public string RequestId { get; set; } = string.Empty;
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Creates an error  with the specified message, errors, and status code.
        /// </summary>
        public static BaseApiResponse<T> ErrorResult(string message, IEnumerable<string> errors) =>
            new BaseApiResponse<T>
            {
                Success = false,
                Status = "Error",
                Message = message,
                Error = message,
                ErrorDetails = errors != null ? string.Join("; ", errors) : string.Empty,
                Errors = errors != null ? errors.ToList().AsReadOnly() : Array.Empty<string>(),
                Timestamp = DateTime.UtcNow,
            };

        public static BaseApiResponse<T> ErrorResult(
            string message,
            IEnumerable<string> errors,
            string status,
            string type,
            string source,
            string requestId) =>
            new BaseApiResponse<T>
            {
                Success = false,
                Status = status,
                Type = type,
                Source = source,
                Message = message,
                Error = message,
                ErrorDetails = errors != null ? string.Join("; ", errors) : string.Empty,
                Errors = errors != null ? errors.ToList().AsReadOnly() : Array.Empty<string>(),
                RequestId = requestId,
                Timestamp = DateTime.UtcNow,
            };

        /// <summary>
        /// Creates a not found  with the specified message.
        /// </summary>
        public static BaseApiResponse<T> NotFoundResult(string message) =>
            new BaseApiResponse<T>
            {
                Success = false,
                Status = "Error",
                Message = message,
                Error = message,
                ErrorDetails = message,
                Errors = new List<string> { message }.AsReadOnly(),
                Timestamp = DateTime.UtcNow,
            };

        /// <summary>
        /// Creates a successful  with the specified data and message.
        /// </summary>
        public static BaseApiResponse<T> SuccessResult(T data, string message) =>
            new BaseApiResponse<T>
            {
                Success = true,
                Status = "Success",
                Message = message,
                Data = data,
                Timestamp = DateTime.UtcNow,
            };

        /// <summary>
        /// Creates a successful  with the specified data and default message.
        /// </summary>
        public static BaseApiResponse<T> SuccessResult(T data) =>
            SuccessResult(data, "Operation completed successfully");
    }
}