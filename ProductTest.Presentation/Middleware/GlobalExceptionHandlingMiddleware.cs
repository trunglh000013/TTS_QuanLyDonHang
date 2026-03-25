using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Common.Exceptions;
using ProductTest.Application.DTOs;

namespace ProductTest.Presentation.Middleware
{
    /// <summary>
    /// Global exception handling middleware for consistent error responses
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred. RequestId: {RequestId}",
                    httpContext.TraceIdentifier);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("Response already started, rethrowing exception. RequestId: {RequestId}", context.TraceIdentifier);
                throw exception;
            }

            context.Response.ContentType = "application/json";
            var requestId = context.TraceIdentifier;

            int statusCode;
            string message;
            List<string> errors = new();
            string? errorType = null;

            switch (exception)
            {
                case ConflictException conflictEx:
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = conflictEx.Message;
                    errorType = "Conflict";
                    errors.Add(conflictEx.Message);
                    break;
                case ArgumentException argEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = argEx.Message;
                    errorType = "BadRequest";
                    errors.Add(argEx.Message);
                    break;
                case UnauthorizedAccessException unauthorizedEx:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "Unauthorized access";
                    errorType = "Unauthorized";
                    errors.Add(unauthorizedEx.Message);
                    break;
                case KeyNotFoundException notFoundEx:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = notFoundEx.Message;
                    errorType = "NotFound";
                    errors.Add(notFoundEx.Message);
                    break;
                case InvalidOperationException invalidOpEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = invalidOpEx.Message;
                    errorType = "InvalidOperation";
                    errors.Add(invalidOpEx.Message);
                    break;
                case TimeoutException timeoutEx:
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    message = "Request timeout";
                    errorType = "Timeout";
                    errors.Add(timeoutEx.Message);
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An internal server error occurred";
                    errorType = "ServerError";
                    errors.Add("Please contact support if the problem persists");
                    break;
            }

            var responseObj = BaseApiResponse<object>.ErrorResult(
                message: message,
                errors: errors,
                status: statusCode.ToString(),
                type: errorType ?? "Error",
                source: context.Request.Path.ToString(),
                requestId: requestId);

            context.Response.StatusCode = statusCode;

            var jsonResponse = JsonSerializer.Serialize(responseObj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    /// <summary>
    /// Extension method to register the global exception middleware
    /// </summary>
    public static class GlobalExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}