namespace AuthPractice.Api.Results.Concretes;
public class Result : Contracts.IResult
{
    public Result()
    {
        
    }
    public Result(int statusCode, string message):this()
    {
        StatusCode = statusCode;
        Message = message;
    }
    public Result(int statusCode, string message, bool success):this()
    {
        StatusCode = statusCode;
        Message = message;
        Success = success;
    }

    public Result(int statusCode, List<string> messages, bool success) : this()
    {
        StatusCode = statusCode;
        Messages = messages;
        Success = success;
    }

    public Result(int statusCode, List<string> errors):this()
    {
        StatusCode = statusCode;
        Errors = errors;
        Success = false;
    }

    public int StatusCode { get; }

    public string Message { get; }

    public bool Success { get; }

    public List<string> Errors { get; }

    public List<string> Messages { get; }
}

