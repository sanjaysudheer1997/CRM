using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Core.Exceptions;

public class ApiExceptionMiddleware
{
  private readonly RequestDelegate next;
  private readonly ILogger<ApiExceptionMiddleware> logger;

  public ApiExceptionMiddleware(
      RequestDelegate next,
      ILogger<ApiExceptionMiddleware> logger)
  {
    this.next = next;
    this.logger = logger;
  }

  public async Task Invoke(HttpContext context, IHostEnvironment env)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "error");
      await HandleExceptionAsync(context, ex, env);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
  {
    string Error;

    logger.LogError(exception.ToString());

    if (exception is ApplicationException)
    {
      Error = "ApplicationError";
      context.Response.StatusCode = (int)HttpStatusCode.Conflict;
    }
    else if (exception is UnauthorizedAccessException)
    {
      Error = "Unauthorized";
      context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
    else
    {
      Error = "Unhandled";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }

    string result = JsonSerializer.Serialize(new
    {
      Error,
      exception.Message,
    });

    context.Response.ContentType = "application/json";
    return context.Response.WriteAsync(result);
  }
}

