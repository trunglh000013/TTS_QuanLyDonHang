using System.Globalization;

namespace ProductTest.Presentation.Middleware;

public sealed class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startedAt = DateTime.UtcNow;

        logger.LogInformation(
            "Request started {TraceId} {RequestMethod} {RequestPath}{QueryString} from {RemoteIp} with culture {Culture}",
            context.TraceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString.Value,
            context.Connection.RemoteIpAddress?.ToString(),
            CultureInfo.CurrentCulture.Name);

        try
        {
            await next(context);

            var elapsedMilliseconds = (DateTime.UtcNow - startedAt).TotalMilliseconds;

            logger.LogInformation(
                "Request completed {TraceId} {RequestMethod} {RequestPath} with status code {StatusCode} in {ElapsedMilliseconds:0.0000} ms",
                context.TraceIdentifier,
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsedMilliseconds);
        }
        catch (Exception exception)
        {
            var elapsedMilliseconds = (DateTime.UtcNow - startedAt).TotalMilliseconds;

            logger.LogError(
                exception,
                "Unhandled exception for request {TraceId} {RequestMethod} {RequestPath}{QueryString} after {ElapsedMilliseconds:0.0000} ms",
                context.TraceIdentifier,
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString.Value,
                elapsedMilliseconds);

            throw;
        }
    }
}
