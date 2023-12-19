using System.Net;

namespace Jwt.Api.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            string message = ex.Message.ToString();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ExceptionModel model = new()
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            };

            await context.Response.WriteAsJsonAsync<ExceptionModel>(model);
        }
    }
}
