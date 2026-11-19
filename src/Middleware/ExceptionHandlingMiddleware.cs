using System.Text.Json;

namespace Messenger.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unhandled exception while processing {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;
            var message = "internal server error";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message,
                statusCode = 500
            }));
        }
    }
    
}