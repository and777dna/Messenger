using System.Text.Json;

namespace Messenger.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;
            var message = "internal server error";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message,
                statusCode = 500
            }));
            logger.Log(LogLevel.Error, message);
        }
    }
    
}