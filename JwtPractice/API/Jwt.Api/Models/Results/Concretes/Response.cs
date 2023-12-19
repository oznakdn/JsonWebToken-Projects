namespace Jwt.Api.Models.Results.Concretes;

public record Response : IResponse
{
    public Response(string message, bool success)
    {
        Message = message; Success = success;
    }

    public Response(IEnumerable<string> errors)
    {
        Errors = errors; Success = false;
    }

    public string Message { get; }
    public IEnumerable<string> Errors { get; }
    public bool Success { get; }
}

