using System.Net;
using System.Text.Json;
using CleanMind.Application.Exceptions;

namespace CleanMind.API.MIddlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex);
        }
    }

    private Task HandleException(HttpContext httpContext, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        var result = string.Empty;

        switch (exception)
        {
            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound; //404
                break;

            case CustomValidationException customValidationException:
                httpStatusCode = HttpStatusCode.BadRequest; //400
                result = JsonSerializer.Serialize(customValidationException.ValidationErrors);
                break;
        }

        httpContext.Response.StatusCode = (int)httpStatusCode;
        return httpContext.Response.WriteAsync(result);
    }

}
