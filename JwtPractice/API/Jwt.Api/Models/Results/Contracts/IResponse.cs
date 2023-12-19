namespace Jwt.Api.Models.Results.Contracts;

public interface IResponse
{
    string Message { get;}
    IEnumerable<string> Errors { get;}
    bool Success { get;}
}

