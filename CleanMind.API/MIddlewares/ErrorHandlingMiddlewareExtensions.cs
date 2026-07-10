namespace CleanMind.API.MIddlewares;

public static class ErrorHandlingMiddlewareExtensions
    {
    public static IApplicationBuilder UseCustomExceptionHandler ( this IApplicationBuilder builder )
        {
        var result = builder.UseMiddleware<ErrorHandlingMiddleware>();
        return result;
        }
    }