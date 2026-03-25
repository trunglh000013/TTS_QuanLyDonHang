using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProductTest.Application.Common.Behaviors;

public sealed class RequestLoggingBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        logger.LogInformation(
            "Handling application request {RequestName} with payload {@Request}",
            requestName,
            request);

        try
        {
            var response = await next();

            stopwatch.Stop();

            logger.LogInformation(
                "Completed application request {RequestName} in {ElapsedMilliseconds} ms",
                requestName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            stopwatch.Stop();

            logger.LogWarning(
                "Application request {RequestName} was cancelled after {ElapsedMilliseconds} ms",
                requestName,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
        catch (Exception exception)
        {
            stopwatch.Stop();

            logger.LogError(
                exception,
                "Application request {RequestName} failed after {ElapsedMilliseconds} ms with payload {@Request}",
                requestName,
                stopwatch.ElapsedMilliseconds,
                request);

            throw;
        }
    }
}
