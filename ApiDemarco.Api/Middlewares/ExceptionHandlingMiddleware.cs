using System.Net;
using ApiDemarco.Application.Exceptions;

namespace ApiDemarco.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (ApiRequestException ex)
    {
      logger.LogError(ex, "API request failed.");
      context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
      await context.Response.WriteAsJsonAsync(new { message = ex.Message });
    }
    catch (DatabaseOperationException ex)
    {
      logger.LogError(ex, "Database operation failed.");
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      await context.Response.WriteAsJsonAsync(new { message = ex.Message });
    }
    catch (Exception ex)
    {
      int statusCode = (int)HttpStatusCode.InternalServerError;
      var message = ex.Message;

      if (ex is IStatusCodeException statusCodeException)
      {
        statusCode = statusCodeException.StatusCode;
        message = ex.Message;
      }

      logger.LogError(ex, "An error occurred.");

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";
      await context.Response.WriteAsJsonAsync(new { message });
    }
  }
}