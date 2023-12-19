

namespace Jwt.Api.Response.Contracts;

public interface IResponse
{
    int StatusCode {get;}
    string Message {get;}
}
