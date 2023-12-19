using Jwt.Api.Response.Contracts;

namespace Jwt.Api.Response.Concretes;

public class Response : IResponse
{
  
    public Response(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; }
    public string Message { get; }
}
