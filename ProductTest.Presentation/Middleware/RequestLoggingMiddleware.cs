using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Options;
using Serilog.Context;

namespace ProductTest.Presentation.Middleware;

public sealed class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger,
    IOptions<RequestLoggingOptions> options
    )
{
    private readonly RequestLoggingOptions _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    public async Task InvokeAsync(HttpContext context)
    {
        var startedAt = DateTime.UtcNow;

        using (LogContext.PushProperty("TraceId", Activity.Current?.Id ?? context.TraceIdentifier))
        {
            logger.LogInformation(_options.RequestStartedTemplate,
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
                    _options.RequestCompletedTemplate,
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
                    _options.RequestErrorTemplate,
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString.Value,
                    elapsedMilliseconds);
                throw;
            }
        }
    }
}

public class RequestLoggingOptions
{
    public string RequestStartedTemplate { get; set; } = default!;
    public string RequestCompletedTemplate { get; set; } = default!;
    public string RequestErrorTemplate { get; set; } = default!;
}
