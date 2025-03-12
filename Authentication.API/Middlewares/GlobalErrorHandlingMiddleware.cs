using Authentication.Application.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Authentication.API.Middlewares;

internal sealed class GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(
        "{dateTime}--Message with TraceId {traceId} failed with message: \n{exceptionMessage}\n{innerException}",
        DateTime.Now,
        httpContext.TraceIdentifier,
        exception.Message,
        exception.InnerException
        );

        await HandleExceptionAsync(httpContext, exception);
        return true;
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(JsonSerializer.Serialize(Error.InternalServerError(ex.Message)));
    }
}
