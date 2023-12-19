using System.Text.Json;

namespace Jwt.Api.GlobalException;

public class ExceptionHandler : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ExceptionResponse response = new();
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            context.Response.ContentType = "application/json";
            switch (context.Response.StatusCode)
            {
                case StatusCodes.Status500InternalServerError:
                    response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                    response.Message = "An error was encountered";
                    break;
                case StatusCodes.Status401Unauthorized:
                    response.StatusCode = (int)StatusCodes.Status401Unauthorized;
                    response.Message = "You should be login!";
                    break;
                case StatusCodes.Status403Forbidden:
                    response.StatusCode = (int)StatusCodes.Status403Forbidden;
                    response.Message = "You are not authorized";
                    break;
                default:
                    break;
            }
            throw;
        }
        await context.Response.WriteAsync(JsonSerializer.Serialize<ExceptionResponse>(response));
    }

}

